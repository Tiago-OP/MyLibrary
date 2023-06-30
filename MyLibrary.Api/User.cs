namespace MyLibrary.Api.Tests;

public record User
{
    public string Name { get; init; }
    public string Email { get; init; }
    public int Id { get; set; }
}