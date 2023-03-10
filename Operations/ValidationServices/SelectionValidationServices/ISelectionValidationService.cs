using Operations.Operations.Bet;
using Operations.Operations.Shared;

namespace Operations.ValidationServices.SelectionValidationServices;

public interface ISelectionValidationService
{
	void ValidateUniqueSelections(IList<ValidationResponse.Selection> selectionsResponse,
		IList<IGrouping<int, MakeBetInput.Selection>> uniqueSelections);

	void ValidateDuplicatedSelections(IList<ValidationResponse.Selection> selectionsResponse,
		IList<IGrouping<int, MakeBetInput.Selection>> duplicateSelections);
}