﻿namespace ExpenseTracker.Domain.Dto;

public record LoginRequestDto(
    string Email,
    string Password);
