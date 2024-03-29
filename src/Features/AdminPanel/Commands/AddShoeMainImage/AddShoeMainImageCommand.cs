﻿using MediatR;

namespace ScriptShoesAPI.Features.AdminPanel.Commands.AddShoeMainImage;

public record AddShoeMainImageCommand : IRequest
{
    public IFormFile File { get; set; }
    public string ShoeName { get; set; }
}