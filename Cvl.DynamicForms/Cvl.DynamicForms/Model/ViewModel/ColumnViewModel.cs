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
        //wartość komórki
        public string Value { get; set; }
        public bool IsBigString { get; set; }

        public string EditUrl { get; internal set; }
        public string MainObjectId { get; internal set; }
        public string MainObjectType { get; internal set; }
        public string BindingPath { get; internal set; }
    }

    public class RowViewModel
    {
        public CellViewModel[] Cells { get; set; }
        public string Id { get;  set; }
        public string ElementTypeFullName { get; set; }
        public string EditUrl { get; internal set; }
    }
}
