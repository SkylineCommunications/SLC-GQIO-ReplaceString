using System;
using Skyline.DataMiner.Analytics.GenericInterface;

[GQIMetaData(Name = "Replace String")]
public class MyCustomOperator : IGQIRowOperator, IGQIInputArguments
{
    private GQIColumnDropdownArgument _stringColumnArg = new GQIColumnDropdownArgument("Column") { IsRequired = true, Types = new GQIColumnType[] { GQIColumnType.String } };
    private GQIStringArgument _inputArgument = new GQIStringArgument("Input") { IsRequired = true};
    private GQIStringArgument _outputArgument = new GQIStringArgument("Output") { IsRequired = false};

    private string _input;
    private string _output;
    private GQIColumn _stringColumn;

    public GQIArgument[] GetInputArguments()
    {
        return new GQIArgument[] { _stringColumnArg, _inputArgument, _outputArgument };
    }

    public OnArgumentsProcessedOutputArgs OnArgumentsProcessed(OnArgumentsProcessedInputArgs args)
    {
        _stringColumn = args.GetArgumentValue(_stringColumnArg);
        _input = args.GetArgumentValue(_inputArgument);
        _output = args.GetArgumentValue(_outputArgument);
        return new OnArgumentsProcessedOutputArgs();
    }

    public void HandleRow(GQIEditableRow row)
    {
        try
        {
            string value = Convert.ToString(row.GetValue(_stringColumn.Name));
            row.SetValue(_stringColumn, value.Replace(_input, _output));
        }
        catch (Exception)
        {
            // Catch empty cells
        }
    }
}