<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;
use Illuminate\Support\Facades\DB;

class CreateTableFunctionalUnits extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('functional_units', function (Blueprint $table) {
            $table->id();
            $table->string('name', 100);
        });

        DB::table('functional_units')->insert(
            [
                ['name' => 'Функц. поразделение #1'],
                ['name' => 'Функц. поразделение #2'],
                ['name' => 'Функц. поразделение #3'],
                ['name' => 'Функц. поразделение #4'],
                ['name' => 'Функц. поразделение #5'],
                ['name' => 'Функц. поразделение #6'],
                ['name' => 'Функц. поразделение #7'],
                ['name' => 'Функц. поразделение #8'],
                ['name' => 'Функц. поразделение #9'],
                ['name' => 'Функц. поразделение #10'],
                ['name' => 'Функц. поразделение #11'],
                ['name' => 'Функц. поразделение #12'],
                ['name' => 'Функц. поразделение #13'],
                ['name' => 'Функц. поразделение #14'],
                ['name' => 'Функц. поразделение #15'],
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
        Schema::dropIfExists('functional_units');
    }
}
