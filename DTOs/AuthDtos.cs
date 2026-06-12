namespace ComicCrazy.DTOs; // DTO used as a simple data container for requests/responses,
                           // that dont expose fields like usernames or passwords

public record RegisterRequest(string Username,string Email, string Password);
public record LoginRequest(string Email, string Password);
public record AuthResponse(string Token,string Username, string Role);
