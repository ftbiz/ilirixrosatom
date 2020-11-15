<?php

namespace App\Http\Controllers;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\Task;
use App\Models\TaskDependencies;

class TaskController extends Controller
{
    private $tasks;
    private $default_tasks;
    private $current_id;

    public function list()
    {

        $startedTask = Task::doesntHave('parents')->select('external_id', 'name')->limit(1)->get(); // Задачи, с которых начинается цепочка
        $this->getAllTasksChainRecurs($startedTask);

//       $allTasks = $this->getAllTasks();
//       $this->default_tasks = $allTasks;
//       $this->createChain($allTasks);

      //  $this->getAllTasksChain($startedTask);

        return response()->json($this->tasks);


    }

    public function getAllTasksChainRecurs($startedTask)
    {
        foreach ($startedTask as $key => $task) {
            $count = $task->children()->with('parents')->whereNotIn('external_id', [$task->external_id])->count();
            if ($count > 1) {
                $task->is_multi_parent = true;
            }

            $this->tasks[$key][] = $task;

            if ($count > 0) {
                $children = $task->children()->with('parents')->whereNotIn('external_id', [$task->external_id])->select('external_id', 'name')->get();
                $this->getAllTasksChainRecurs($children);
            }

           // return response()->json($task->children()->get());
        }
    }

    public function getAllTasksChain()
    {
        $tasks = Task::select('external_id')->get();

        foreach ($tasks as $task){
            $parents = $task->parents()->select('external_id')->get();
            if ($task->children()->count() > 1) {
                $task->is_multi_parent = true;
            }
            if($task->parents()->count() > 0){
                foreach ($parents as $parent){
                    $task->parent = $parent->external_id;
                    $tasksChain[$parent->external_id . '_' . $task->external_id] = $task;
                }
            }else{
                $tasksChain['first' . '_' . $task->external_id] = $task;
            }
        }
    }

    public function getAllTasks()
    {
        $tasks = Task::select('external_id', 'name')->get();
        $allTasks = [];
        foreach ($tasks as $task){
            //$taskData = ['external_id' => $task->external_id];
            if ($task->children()->count() > 1) {
                $task->is_multi_parent = true;
            }
            $task->getParents = $task->parents()->select('external_id')->get()->toArray();
            $task->getChildren = $task->children()->select('external_id')->get()->toArray();
            $allTasks[$task->external_id] = $task->toArray();
        }
        return $allTasks;
    }

    public function createChain($allTasks)
    {
        foreach ($allTasks as $task){

            if(empty($this->default_tasks[$task['external_id']]['getParents'])){
                $this->tasks[$task['external_id']][] = $this->default_tasks[$task['external_id']];
                $this->current_id = $task['external_id'];
                continue;
            }

            if(!empty($this->default_tasks[$task['external_id']]['getChildren'])){
                foreach ($this->default_tasks[$task['external_id']]['getChildren'] as $child){
                    if($child['external_id'] == $task['external_id']){
                        continue;
                    }
                    $newChild = $this->default_tasks[$child['external_id']];
                    $this->tasks[$this->current_id][] = $newChild;
                    if(!empty($newChild['getChildren'])){
//                        ob_start();
//                        print_r($this->tasks);
//                        $answer=ob_get_clean();
//                        $h=fopen($_SERVER["DOCUMENT_ROOT"]."/test.txt","w+");
//                        fwrite($h,$answer);
                        $this->createChain($newChild['getChildren']);
                    }

                }
            }
        }

    }

}
