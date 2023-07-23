using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ryvarr.models;

public partial class DataValidator : ObservableObject
{
    private ObservableValidator _model;

    [ObservableProperty]
    private List<string> _errorMessages = new List<string>();
    public DataValidator(ObservableValidator model) {
        _model = model;
    }

    public void OnError(object? sender, DataErrorsChangedEventArgs e)
    {
        if (_model.HasErrors)
        {
            IEnumerable<ValidationResult> entityErrors = _model.GetErrors();
            // Now, extract the error strings from each ValidationResult
            List<string> errorMessages = new List<string>();
            // OR for entity-level errors
            foreach (var validationResult in entityErrors)
            {
                if (!string.IsNullOrEmpty(validationResult.ErrorMessage))
                    errorMessages.Add("* " + validationResult.ErrorMessage);
            }
            ErrorMessages = errorMessages;
        }
    }
    public void Validate()
    {
        if (!_model.HasErrors)
        {
            ErrorMessages = new List<string>();
        }
    }
}
