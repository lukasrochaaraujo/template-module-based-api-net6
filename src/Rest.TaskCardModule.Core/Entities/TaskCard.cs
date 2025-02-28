﻿using System;
using System.Collections.Generic;

namespace Rest.TaskCardModule.Core.Entities;

public class TaskCard
{
    public string Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Status Status { get; private set; }
    public Priority Priority { get; private set; }
    public IReadOnlyCollection<TaskComment> Comments { get => (IReadOnlyCollection<TaskComment>)_comments; }
    private ICollection<TaskComment> _comments;
    public DateTime Created { get; private set; }
    public DateTime? Started { get; private set; }
    public DateTime? Finished { get; private set; }

    public TaskCard(string title, string description, Priority priority)
    {
        Title = title;
        Description = description;
        Status = Status.Todo;
        Priority = priority;
        Created = DateTime.Now;
        _comments = new List<TaskComment>();
    }

    public void Start()
    {
        if (Status == Status.Todo)
        {
            Status = Status.InProgress;
            Started = DateTime.Now;
        }
    }

    public void Cancel(string comment, string commentedBy)
    {
        if (Status != Status.Done || Status != Status.Cancelled)
        {
            Status = Status.Cancelled;
            Finished = DateTime.Now;
            AddComment(comment, commentedBy);
        }
    }

    public void Done()
    {
        if (Status != Status.Done && Status == Status.InProgress)
        {
            Status = Status.Done;
            Finished = DateTime.Now;
        }
    }

    public void AddComment(string comment, string commentedBy)
    {
        if (!string.IsNullOrWhiteSpace(comment) && !string.IsNullOrWhiteSpace(commentedBy))
        {
            _comments.Add(new TaskComment(comment, commentedBy));
        }
    }
}
