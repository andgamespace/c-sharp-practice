using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using TodoList.Services;

namespace TodoList.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly TaskService _taskService;
    private ObservableCollection<TaskViewModel> _tasks;
    private TaskViewModel? _selectedTask;
    private TaskViewModel _newTask;
    private bool _isTaskFormVisible;
    private bool _isEditMode;
    private string _filterText = string.Empty;
    private int _selectedPriorityFilter = -1; // -1 means "All"
    private bool _showCompletedTasks = true;

    public ObservableCollection<TaskViewModel> Tasks
    {
        get => _tasks;
        set => this.RaiseAndSetIfChanged(ref _tasks, value);
    }

    public ObservableCollection<TaskViewModel> FilteredTasks => new(
        _tasks.Where(t => 
            (string.IsNullOrEmpty(_filterText) || t.Title.Contains(_filterText, StringComparison.OrdinalIgnoreCase) || 
            (t.Description != null && t.Description.Contains(_filterText, StringComparison.OrdinalIgnoreCase))) &&
            (_selectedPriorityFilter == -1 || (int)t.Priority == _selectedPriorityFilter) &&
            (_showCompletedTasks || !t.IsCompleted)
        )
    );
    
    public TaskViewModel? SelectedTask
    {
        get => _selectedTask;
        set => this.RaiseAndSetIfChanged(ref _selectedTask, value);
    }

    public TaskViewModel NewTask
    {
        get => _newTask;
        set => this.RaiseAndSetIfChanged(ref _newTask, value);
    }

    public bool IsTaskFormVisible
    {
        get => _isTaskFormVisible;
        set => this.RaiseAndSetIfChanged(ref _isTaskFormVisible, value);
    }

    public bool IsEditMode
    {
        get => _isEditMode;
        set => this.RaiseAndSetIfChanged(ref _isEditMode, value);
    }

    public string FilterText
    {
        get => _filterText;
        set
        {
            this.RaiseAndSetIfChanged(ref _filterText, value);
            this.RaisePropertyChanged(nameof(FilteredTasks));
        }
    }

    public int SelectedPriorityFilter
    {
        get => _selectedPriorityFilter;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedPriorityFilter, value);
            this.RaisePropertyChanged(nameof(FilteredTasks));
        }
    }

    public bool ShowCompletedTasks
    {
        get => _showCompletedTasks;
        set
        {
            this.RaiseAndSetIfChanged(ref _showCompletedTasks, value);
            this.RaisePropertyChanged(nameof(FilteredTasks));
        }
    }

    // Commands
    public ReactiveCommand<Unit, Unit> AddNewTaskCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveTaskCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelEditCommand { get; }
    public ReactiveCommand<TaskViewModel, Unit> EditTaskCommand { get; }
    public ReactiveCommand<TaskViewModel, Unit> DeleteTaskCommand { get; }
    public ReactiveCommand<TaskViewModel, Unit> ToggleTaskCompletionCommand { get; }
    public ReactiveCommand<Unit, Unit> RefreshTasksCommand { get; }

    public MainWindowViewModel()
    {
        _taskService = new TaskService();
        _tasks = new ObservableCollection<TaskViewModel>();
        _newTask = new TaskViewModel();

        // Explicitly create each command with proper types
        
        // Add new task command - action returns void/Unit
        AddNewTaskCommand = ReactiveCommand.Create<Unit, Unit>(_ => 
        {
            NewTask = new TaskViewModel();
            IsTaskFormVisible = true;
            IsEditMode = false;
            return Unit.Default; // Explicitly return Unit
        });

        // Save task command - wraps async void method
        SaveTaskCommand = ReactiveCommand.CreateFromTask<Unit, Unit>(_ => 
        {
            return Task.Run(async () => 
            {
                await SaveTaskAsyncWrapper();
                return Unit.Default;
            });
        });
        
        // Cancel edit command - action returns void/Unit
        CancelEditCommand = ReactiveCommand.Create<Unit, Unit>(_ => 
        {
            IsTaskFormVisible = false;
            return Unit.Default; // Explicitly return Unit
        });
        
        // Edit task command - action returns void/Unit
        EditTaskCommand = ReactiveCommand.Create<TaskViewModel, Unit>(task => 
        {
            NewTask = new TaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                IsCompleted = task.IsCompleted,
                Priority = task.Priority,
                CreatedAt = task.CreatedAt,
                CompletedAt = task.CompletedAt
            };
            IsTaskFormVisible = true;
            IsEditMode = true;
            return Unit.Default; // Explicitly return Unit
        });

        // Delete task command - wraps async void method
        DeleteTaskCommand = ReactiveCommand.CreateFromTask<TaskViewModel, Unit>(task => 
        {
            return Task.Run(async () => 
            {
                await DeleteTaskAsyncWrapper(task);
                return Unit.Default;
            });
        });
        
        // Toggle completion command - wraps async void method
        ToggleTaskCompletionCommand = ReactiveCommand.CreateFromTask<TaskViewModel, Unit>(task => 
        {
            return Task.Run(async () => 
            {
                await ToggleTaskCompletionAsyncWrapper(task);
                return Unit.Default;
            });
        });
        
        // Refresh command - wraps async void method
        RefreshTasksCommand = ReactiveCommand.CreateFromTask<Unit, Unit>(_ => 
        {
            return Task.Run(async () => 
            {
                await LoadTasksAsyncWrapper();
                return Unit.Default;
            });
        });

        // Load tasks initially
        Task.Run(LoadTasksAsync);
    }

    // Helper wrapper methods to ensure void returns
    private async Task SaveTaskAsyncWrapper()
    {
        await SaveTaskAsync();
    }
    
    private async Task DeleteTaskAsyncWrapper(TaskViewModel task)
    {
        await DeleteTaskAsync(task);
    }
    
    private async Task ToggleTaskCompletionAsyncWrapper(TaskViewModel task)
    {
        await ToggleTaskCompletionAsync(task);
    }
    
    private async Task LoadTasksAsyncWrapper()
    {
        await LoadTasksAsync();
    }

    private async Task LoadTasksAsync()
    {
        var tasks = await _taskService.GetAllTasksAsync();
        Tasks = new ObservableCollection<TaskViewModel>(
            tasks.Select(t => new TaskViewModel(t))
        );
        this.RaisePropertyChanged(nameof(FilteredTasks));
    }

    private async Task SaveTaskAsync()
    {
        if (string.IsNullOrWhiteSpace(NewTask.Title))
        {
            // In a real app, you would show validation errors
            return;
        }

        var taskModel = NewTask.ToModel();

        if (IsEditMode)
        {
            await _taskService.UpdateTaskAsync(taskModel);
        }
        else
        {
            await _taskService.AddTaskAsync(taskModel);
        }

        IsTaskFormVisible = false;
        await LoadTasksAsync();
    }

    private async Task DeleteTaskAsync(TaskViewModel task)
    {
        await _taskService.DeleteTaskAsync(task.Id);
        await LoadTasksAsync();
    }

    private async Task ToggleTaskCompletionAsync(TaskViewModel task)
    {
        await _taskService.ToggleTaskCompletionAsync(task.Id);
        await LoadTasksAsync();
    }
}