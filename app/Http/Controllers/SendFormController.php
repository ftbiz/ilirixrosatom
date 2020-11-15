<?php

namespace App\Http\Controllers;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\Task;
use App\Models\TaskDependencies;
use Carbon\Carbon;

class SendFormController extends Controller
{
    public function send()
    {

        return view('send_import');
    }

    public function testing()
    {
//        $task = new Task();
//        $task->name = '32112';
//        $task->external_id = '32112';
//        $task->plan_date_start = Carbon::parse('04.11.2019');
//        $task->plan_duration = '3';
//        $task->duration_min = '1';
//        $task->cost_minus_move = '1';
//        $task->cost_plus_move = '1';
//        $task->cost_duration_min = '1';
//        $task->functional_unit_id = '1';
//        $task->milestone_id = '1';
//
//        $task->save();
//
//        $taskDependencies = new TaskDependencies();
//        $taskDependencies->task_id = 3211;
//        $taskDependencies->parent_task_id = 3211;
//        $taskDependencies->save();

        $task = TaskDependencies::where('parent_task_id', '>', 0)->delete();
        $task = Task::where('external_id', '>', 0)->delete();

        dd($task);
    }
}
