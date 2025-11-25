using Domains.ValueObjects;

namespace Applications.Dtos;

public record UserDto(Guid Id, Username Username);