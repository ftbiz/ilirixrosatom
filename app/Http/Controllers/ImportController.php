<?php

namespace App\Http\Controllers;


use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Lib\Files\OpenCsv;
use Illuminate\Support\Facades\Storage;
use App\Lib\Tasks\Import;

class ImportController extends Controller
{
    public function import(Request $request)
    {
        $file = $request->file('import');
        $newFileName = time().'_'.$file->getClientOriginalName();
        $file->move(storage_path('import'), $newFileName);

        $storageFilePath = Storage::disk('import')->path($newFileName);
        $open = new OpenCsv($storageFilePath);
        $fileInfo = $open->openFile();
        $dataImport = $this->prepareFile($fileInfo);

        $result = new Import($dataImport);

        $lol = 2;

        return response()->json(['result'=>$result->result]);
    }

    public function prepareFile($fileInfo){
        unset($fileInfo[0]);
        unset($fileInfo[1]);
        unset($fileInfo[2]);
        unset($fileInfo[3]);
        unset($fileInfo[4]);
        return $fileInfo;
    }
}
