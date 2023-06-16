namespace WeRecruit.Entities;

public class Submission
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int LevelOfStudies { get; set; }
    public int YearsOfExperience { get; set; }
    public string LastEmployer { get; set; }
    public string ResumeDirectoryName { get; set; }
}