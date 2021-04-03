namespace Cvl.DynamicForms.Model
{
    public class ColumnViewModel
    {
        /// <summary>
        /// Nazwa propercji lub ścieżka do propercji 
        /// np. Name
        /// Person.Name
        /// </summary>
        public string BindingPath { get; set; }

        public string Header { get; set; }
        public string Description { get; set; }

        public string ToolTip { get; set; }
    }

    public class CellViewModel
    {
        public object Value { get; set; }
    }

    public class RowViewModel
    {
        public CellViewModel[] Cells { get; set; }
        public string Id { get;  set; }
        public string ElementTypeFullName { get; set; }
    }
}
