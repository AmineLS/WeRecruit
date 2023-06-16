namespace WeRecruit.Dto;

public record SubmissionDto(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string LevelOfStudies,
    int YearsOfExperience,
    string LastEmployer,
    IFormFile Resume
);