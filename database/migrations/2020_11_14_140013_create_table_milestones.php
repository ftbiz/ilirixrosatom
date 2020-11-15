<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;
use Illuminate\Support\Facades\DB;

class CreateTableMilestones extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('milestones', function (Blueprint $table) {
            $table->id();
            $table->string('name', 100);
        });

        DB::table('milestones')->insert(
            [
                ['name' => 'Вехо #1'],
                ['name' => 'Вехо #2'],
                ['name' => 'Вехо #3'],
                ['name' => 'Вехо #4'],
                ['name' => 'Вехо #5'],
                ['name' => 'Вехо #6'],
                ['name' => 'Вехо #7'],
                ['name' => 'Вехо #8'],
                ['name' => 'Вехо #9'],
                ['name' => 'Вехо #10'],
                ['name' => 'Вехо #11'],
                ['name' => 'Вехо #12'],
                ['name' => 'Вехо #13'],
                ['name' => 'Вехо #14'],
                ['name' => 'Вехо #15'],
            ]
        );
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('milestones');
    }
}
