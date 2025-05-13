using System;
using System.Reactive;
using ReactiveUI;
using TodoList.Models;

namespace TodoList.ViewModels;

public class TaskViewModel : ViewModelBase
{
    private int _id;
    private string _title = string.Empty;
    private string? _description;
    private DateTime _dueDate;
    private bool _isCompleted;
    private Priority _priority;

    public int Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public string? Description
    {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }

    public DateTime DueDate
    {
        get => _dueDate;
        set => this.RaiseAndSetIfChanged(ref _dueDate, value);
    }

    public bool IsCompleted
    {
        get => _isCompleted;
        set => this.RaiseAndSetIfChanged(ref _isCompleted, value);
    }

    public Priority Priority
    {
        get => _priority;
        set => this.RaiseAndSetIfChanged(ref _priority, value);
    }

    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    // Command for toggling task completion
    public ReactiveCommand<Unit, Unit> ToggleCompletionCommand { get; }

    public TaskViewModel()
    {
        _dueDate = DateTime.Now.AddDays(1);
        CreatedAt = DateTime.Now;
        
        // Initialize the toggle command
        ToggleCompletionCommand = ReactiveCommand.Create(() =>
        {
            IsCompleted = !IsCompleted;
            CompletedAt = IsCompleted ? DateTime.Now : null;
        });
    }

    // Constructor to create from a Task model
    public TaskViewModel(Models.Task task)
    {
        _id = task.Id;
        _title = task.Title;
        _description = task.Description;
        _dueDate = task.DueDate;
        _isCompleted = task.IsCompleted;
        _priority = task.Priority;
        CreatedAt = task.CreatedAt;
        CompletedAt = task.CompletedAt;
        
        // Initialize the toggle command
        ToggleCompletionCommand = ReactiveCommand.Create(() =>
        {
            IsCompleted = !IsCompleted;
            CompletedAt = IsCompleted ? DateTime.Now : null;
        });
    }

    // Convert ViewModel back to Model
    public Models.Task ToModel()
    {
        return new Models.Task
        {
            Id = Id,
            Title = Title,
            Description = Description,
            DueDate = DueDate,
            IsCompleted = IsCompleted,
            Priority = Priority,
            CreatedAt = CreatedAt,
            CompletedAt = CompletedAt
        };
    }
}