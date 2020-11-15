<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateTableTaskDependencies extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('task_dependencies', function (Blueprint $table) {
            $table->id();

            $table->unsignedBigInteger('parent_task_id');
            $table->foreign('parent_task_id')->references('external_id')->on('tasks');

            $table->unsignedBigInteger('task_id');
            $table->foreign('task_id')->references('external_id')->on('tasks');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('task_dependencies');
    }
}
