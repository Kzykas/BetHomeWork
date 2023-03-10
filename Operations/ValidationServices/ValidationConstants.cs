namespace Operations.ValidationServices;

public static class ValidationConstants
{
	// Player
	public const int DefaultPlayerBalance = 1000;
	public const string InsufficientBalance = "Insufficient balance";
	public const int InsufficientBalanceCode = 8;
	public const int StakeAmountMaxNumbersAfterDot = 2;
	public const int StakeAmountMaxNumbersAfterDotCode = 12;
	public static readonly string StakeAmountMaxNumbersAfterDotMessage = $"Maximum numbers after comma in stake amount are {StakeAmountMaxNumbersAfterDot}";
	public const decimal MinStakeAmount = 0.3m;
	public const int MinStakeAmountCode = 2;
	public static readonly string MinStakeAmountMessage = $"Minimum stake amount is: {MinStakeAmount}";
	public const decimal MaxStakeAmount = 10000m;
	public const int MaxStakeAmountCode = 3;
	public static readonly string MaxStakeAmountMessage = $"Minimum stake amount is: {MaxStakeAmount}";

	// Section
	public const int DuplicateSectionFoundCode = 10;
	public const string DuplicateSectionFound = "Duplicate Section Found";
	public const int OddsMaxNumbersAfterDot = 3;
	public const int OddsMaxNumbersAfterDotCode = 11;
	public static readonly string OddsMaxNumbersAfterDotMessage = $"Maximum numbers after comma in odds are {OddsMaxNumbersAfterDot}";
	public const decimal MinOdds = 1m;
	public const int MinOddsCode = 5;
	public static readonly string OddsMinMessage = $"Maximum odds are: {MinOdds}";
	public const decimal MaxOdds = 10000m;
	public const int MaxOddsCode = 4;
	public static readonly string MaxOddsMessage = $"Maximum odds are: {MaxOdds}";
	
	// MaxWin
	public const int MaxWinAmount = 2000;
	public const int MaxWinAmountCode = 13;
	public static readonly string MaxWinAmountMessage = $"Max win amount is: {MaxWinAmount}";
}