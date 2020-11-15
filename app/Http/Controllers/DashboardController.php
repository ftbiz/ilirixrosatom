<?php

namespace App\Http\Controllers;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;

class DashboardController extends Controller
{
    public function list()
    {
        return response()->json(['lol'=>'2', 131]);
    }


}
