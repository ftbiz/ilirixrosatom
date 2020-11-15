<?php


namespace App\Lib\Tasks;

use App\Models\Task;
use App\Models\TaskDependencies;
use Carbon\Carbon;

class Import
{
    private $dataImport;
    public $result = false;

//    private $dataKeyNumber = [
//        'null' => 0,
//        'milestone_id' => 1,
//        'external_id' => 2,
//        'name' => 3,
//        'parent_task' => 4,
//        'functional_unit_id' => 5,
//        'plan_date_start' => 6,
//        'plan_duration' => 7,
//        'duration_min' => 8,
//        'fact_date_start' => 9,
//        'fact_date_finish' => 10,
//        'cost_minus_move' => 11,
//        'cost_plus_move' => 12,
//        'cost_duration_min' => 13,
//        'probability_success' => 14,
//
//    ];

    private $dataKeyNumber = [

        'milestone_id' => 0,
        'external_id' => 1,
        'name' => 2,
        'parent_task' => 3,
        'functional_unit_id' => 4,
        'plan_date_start' => 5,
        'plan_duration' => 6,
        'duration_min' => 7,
        'fact_date_start' => 8,
        'fact_date_finish' => 9,
        'cost_minus_move' => 10,
        'cost_plus_move' => 11,
        'cost_duration_min' => 12,
        'probability_success' => 13,

    ];

    public function __construct($dataImport)
    {
        $this->dataImport = $dataImport;
        if($this->tasksImport()){
            $this->result = true;
        }
    }

    private function tasksImport()
    {
        $this->clearTasksBeforeImport();

        foreach ($this->dataImport as $newTask){
            $task = new Task();
            $task->milestone_id = $newTask[$this->dataKeyNumber['milestone_id']];
            $task->external_id = $newTask[$this->dataKeyNumber['external_id']];
            $task->name = $newTask[$this->dataKeyNumber['name']];
            $task->functional_unit_id = !empty($newTask[$this->dataKeyNumber['functional_unit_id']]) ? $newTask[$this->dataKeyNumber['functional_unit_id']] : 1;
            $task->plan_date_start = Carbon::parse($newTask[$this->dataKeyNumber['plan_date_start']]);
            $task->plan_duration = $newTask[$this->dataKeyNumber['plan_duration']];
            $task->duration_min = $newTask[$this->dataKeyNumber['duration_min']];
            $task->fact_date_start = Carbon::parse($newTask[$this->dataKeyNumber['fact_date_start']]);
            $task->fact_date_finish = Carbon::parse($newTask[$this->dataKeyNumber['fact_date_finish']]);
            $task->cost_minus_move = $newTask[$this->dataKeyNumber['cost_minus_move']];
            $task->cost_plus_move = $newTask[$this->dataKeyNumber['cost_plus_move']];
            $task->cost_duration_min = $newTask[$this->dataKeyNumber['cost_duration_min']];
            $task->probability_success = !empty($newTask[$this->dataKeyNumber['probability_success']]) ? $newTask[$this->dataKeyNumber['probability_success']] : 0;
            $task->save();
        }

        foreach ($this->dataImport as $newTask){
            if(empty($newTask[$this->dataKeyNumber['parent_task']])){
                continue;
            }
            $parentTasks = explode(' ', $newTask[$this->dataKeyNumber['parent_task']]);
            foreach ($parentTasks as $parentTask){
                $taskDependencies = new TaskDependencies();
                $taskDependencies->task_id = $newTask[$this->dataKeyNumber['external_id']];
                $taskDependencies->parent_task_id = $parentTask;
                $taskDependencies->save();
            }
        }

        return true;
    }

    private function clearTasksBeforeImport()
    {
        $taskDependencies= TaskDependencies::where('parent_task_id', '>', 0)->delete();
        $task = Task::where('external_id', '>', 0)->delete();
    }
}
