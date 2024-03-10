using HRMS.Domain.Constants;

namespace HRMS.Application.Utility.Handler;

public class GenerateHandler
{
    private static readonly Random Random = new Random();

    public static string GenerateNik(string? code = null)
    {
        if (code is null)
        {
            return "000001"; // First employee
        }

        var generateCode = Convert.ToInt32(code) + 1;

        return generateCode.ToString();
    }

    public static double GenerateTotalScore(int scoreSegment1, int scoreSegment2, int scoreSegment3, int scoreSegment4)
    {
        var totalScore = (double)(scoreSegment1 + scoreSegment2 + scoreSegment3) / 3 / 2 + (double)scoreSegment4 / 2;
        
        return totalScore;
    }

    public static Grade GenerateGradeLevel(int scoreSegment1, int scoreSegment2, int scoreSegment3, int scoreSegment4)
    {
        var grade = GenerateTotalScore(scoreSegment1, scoreSegment2, scoreSegment3, scoreSegment4);
        return grade < 80 ? Grade.B : Grade.A;
    }

    public static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}
