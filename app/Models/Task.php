<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Task extends Model
{
    use HasFactory;

    /**
     * The table associated with the model.
     *
     * @var string
     */
    protected $table = 'tasks';
    public $timestamps = false;
    protected $dateFormat = 'U';
    protected $primaryKey = 'external_id';
    public $incrementing = false;

    public function children(){
        return $this->belongsToMany(Task::class, 'task_dependencies', 'parent_task_id', 'task_id');
    }

    public function parents(){
        return $this->belongsToMany(Task::class, 'task_dependencies', 'task_id', 'parent_task_id');
    }

    public function milestone(){
        return $this->belongsTo(Milestones::class, 'milestone_id', 'id');
    }

    public function functionalUnit(){
        return $this->belongsTo(FunctionalUnits::class, 'functional_unit_id', 'id');
    }
}
