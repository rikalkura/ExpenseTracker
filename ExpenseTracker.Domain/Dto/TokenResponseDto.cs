namespace ExpenseTracker.Domain.Dto;

public record TokenResponseDto(
    string AccessToken, 
    string RefreshToken);