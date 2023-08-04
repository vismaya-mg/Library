using LibraryManagement.Constants;

namespace LibraryManagement.Helpers;

/// <summary>
/// Helper class for getting fine based on days
/// </summary>
public class BookHelper
{
    /// <summary>
    /// Method to get the fine amount
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static int GetFineAmount(DateTime dateTime)
    {
        var dueDays = (DateTime.UtcNow - dateTime).Days;
        switch (dueDays)
        {
            case var days when days > DueDaysConstants.MaximumFineDays:

                return FineAmountConstants.MaximumFineAmount;

            case var days when days > DueDaysConstants.HighFineDays:

                return FineAmountConstants.HighFineAmount;

            case var days when days > DueDaysConstants.ModerateFineDays:

                return FineAmountConstants.ModerateFineAmount;

            default:

                return FineAmountConstants.DefaultFineAmount;
        }
    }
}
