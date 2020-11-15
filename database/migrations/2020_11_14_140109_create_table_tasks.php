<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateTableTasks extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('tasks', function (Blueprint $table) {
            $table->unsignedBigInteger('external_id');
            $table->primary('external_id');

            $table->string('name', 100);
            $table->date('plan_date_start');
            $table->integer('plan_duration');
            $table->integer('duration_min');
            $table->date('fact_date_start')->nullable();
            $table->date('fact_date_finish')->nullable();
            $table->integer('cost_minus_move');
            $table->integer('cost_plus_move');
            $table->integer('cost_duration_min');
            $table->integer('probability_success')->nullable();

            $table->unsignedBigInteger('functional_unit_id');
            $table->foreign('functional_unit_id')->references('id')->on('functional_units');

            $table->unsignedBigInteger('milestone_id');
            $table->foreign('milestone_id')->references('id')->on('milestones');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('tasks');
    }
}
