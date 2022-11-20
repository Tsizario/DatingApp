namespace DatingApp.BLL.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dateOfBirth)
        {
            var currentDay = DateTime.Today;
            var age = currentDay.Year - dateOfBirth.Year;
            var currentDayNYearsAgo = currentDay.AddYears(-age);

            if (dateOfBirth.Date > currentDayNYearsAgo)
                age--;

            return age;
        }
    }
}
