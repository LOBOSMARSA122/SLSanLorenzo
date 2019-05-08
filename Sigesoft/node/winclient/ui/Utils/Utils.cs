using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Sigesoft.Common;
using System.Windows.Forms;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.UI.Operations;

namespace Sigesoft.Node.WinClient.UI
{
    public class Utils
    {
        public static string GetGridColumnsDetail(DataGridView pdgGrilla)
        {
            StringBuilder sb = new StringBuilder();

            foreach (DataGridViewColumn item in pdgGrilla.Columns)
            {
                sb.AppendLine("Col: " + item.Name + " Ancho: " + item.Width.ToString());
            }

            return sb.ToString();
        }

        public static void LoadDropDownList(ComboBox prmDropDownList, string prmDataTextField = null, string prmDataValueField = null, List<KeyValueDTO> prmDataSource = null, DropDownListAction? prmDropDownListAction = null)
        {
            if (prmDataSource == null) return;
            prmDropDownList.DataSource = null;
            prmDropDownList.Items.Clear();
            KeyValueDTO firstItem = null;

            if (prmDropDownListAction != null)
            {
                //prmDropDownList. AppendDataBoundItems = true;
               
                switch (prmDropDownListAction)
                {
                    case DropDownListAction.All:
                        if (prmDataTextField == "Value1")
                        {
                            firstItem = new KeyValueDTO() { Id = Constants.AllValue, Value1 = Constants.All };
                        }
                        else if (prmDataTextField == "Value2")
                        {
                            firstItem = new KeyValueDTO() { Id = Constants.AllValue, Value2 = Constants.All };
                        }
                        else if (prmDataTextField == "Value3")
                        {
                            firstItem = new KeyValueDTO() { Id = Constants.AllValue, Value3 = Constants.All };
                        }                      
                        break;
                    case DropDownListAction.Select:
                        if (prmDataTextField == "Value1")
                        {
                            firstItem = new KeyValueDTO() { Id = Constants.SelectValue, Value1 = Constants.Select };
                        }
                        else if (prmDataTextField == "Value2")
                        {
                            firstItem = new KeyValueDTO() { Id = Constants.SelectValue, Value2 = Constants.Select };
                        }
                        else if (prmDataTextField == "Value3")
                        {
                            firstItem = new KeyValueDTO() { Id = Constants.SelectValue, Value3 = Constants.Select };
                        }                       
                        break;
                }
            }

            if (firstItem != null)
            {
                prmDataSource.Insert(0, firstItem);
            }
            
            if (prmDataSource != null)
            {
                if (prmDataSource.Count != 0)
                {
                    prmDropDownList.DisplayMember = prmDataTextField;
                    prmDropDownList.ValueMember = prmDataValueField;
                    prmDropDownList.DataSource = prmDataSource;                                
                }
            }

        }

        public static void LoadDropDownList(ComboBox prmDropDownList, string prmDataTextField = null, string prmDataValueField = null, List<Sigesoft.Node.WinClient.UI.Operations.FrmEsoV2.StructKeyDto> prmDataSource = null, DropDownListAction? prmDropDownListAction = null)
        {

            prmDropDownList.DataSource = null;
            prmDropDownList.Items.Clear();
            var firstItem = new FrmEsoV2.StructKeyDto();

            if (prmDropDownListAction != null)
            {
                //prmDropDownList. AppendDataBoundItems = true;

                switch (prmDropDownListAction)
                {
                    case DropDownListAction.All:
                        if (prmDataTextField == "Value1")
                        {
                            firstItem = new FrmEsoV2.StructKeyDto() { Id = Constants.AllValue, Value1 = Constants.All };
                        }
                        else if (prmDataTextField == "Value2")
                        {
                            firstItem = new FrmEsoV2.StructKeyDto() { Id = Constants.AllValue, Value2 = Constants.All };
                        }
                        else if (prmDataTextField == "Value3")
                        {
                            firstItem = new FrmEsoV2.StructKeyDto() { Id = Constants.AllValue, Value3 = Constants.All };
                        }
                        break;
                    case DropDownListAction.Select:
                        if (prmDataTextField == "Value1")
                        {
                            firstItem = new FrmEsoV2.StructKeyDto() { Id = Constants.SelectValue, Value1 = Constants.Select };
                        }
                        else if (prmDataTextField == "Value2")
                        {
                            firstItem = new FrmEsoV2.StructKeyDto() { Id = Constants.SelectValue, Value2 = Constants.Select };
                        }
                        else if (prmDataTextField == "Value3")
                        {
                            firstItem = new FrmEsoV2.StructKeyDto() { Id = Constants.SelectValue, Value3 = Constants.Select };
                        }
                        break;
                }
            }
            if (firstItem.Id != null)
            {
                if (prmDataSource != null) prmDataSource.Insert(0, firstItem);
            }

            if (prmDataSource != null)
            {
                if (prmDataSource.Count != 0)
                {
                    prmDropDownList.DisplayMember = prmDataTextField;
                    prmDropDownList.ValueMember = prmDataValueField;
                    prmDropDownList.DataSource = prmDataSource;
                }
            }

        }

        public static void LoadComboTreeBoxList(ComboTreeBox prmDropDownList, List<KeyValueDTOForTree> prmDataSource = null, DropDownListAction? prmDropDownListAction = null)
        {
            prmDropDownList.Nodes.Clear();

            KeyValueDTOForTree firstItem = null;
            if (prmDropDownListAction != null)
            {
                switch (prmDropDownListAction)
                {
                    case DropDownListAction.All:
                        firstItem = new KeyValueDTOForTree() { Id = Constants.AllValue, Value1 = Constants.All };

                        break;
                    case DropDownListAction.Select:
                        firstItem = new KeyValueDTOForTree() { Id = Constants.SelectValue, Value1 = Constants.Select };
                        break;
                }
            }

            if (prmDataSource != null)
            {
                prmDropDownList.Nodes.AddRange(ProcessDataForComboTreeBox(prmDataSource, "-1"));
            }

            if (firstItem != null)
            {
                ComboTreeNode firstNode = new ComboTreeNode(firstItem.Value1);
                firstNode.Tag= firstItem.Id;

                prmDropDownList.Nodes.Insert(0,firstNode);
                prmDropDownList.SelectedNode = firstNode;
            }

            prmDropDownList.ExpandAll();
        }


        public static void LoadComboTreeBoxList_(ComboTreeBox prmDropDownList, List<KeyValueDTOForTree> prmDataSource = null, DropDownListAction? prmDropDownListAction = null)
        {
            prmDropDownList.Nodes.Clear();

            KeyValueDTOForTree firstItem = null;
            if (prmDropDownListAction != null)
            {
                switch (prmDropDownListAction)
                {
                    case DropDownListAction.All:
                        firstItem = new KeyValueDTOForTree() { Id = Constants.AllValue, Value1 = Constants.All };

                        break;
                    case DropDownListAction.Select:
                        firstItem = new KeyValueDTOForTree() { Id = Constants.SelectValue, Value1 = Constants.Select };
                        break;
                }
            }

            if (prmDataSource != null)
            {
                prmDropDownList.Nodes.AddRange(ProcessDataForComboTreeBox(prmDataSource, prmDataSource[0].ParentId));
            }

            if (firstItem != null)
            {
                ComboTreeNode firstNode = new ComboTreeNode(firstItem.Value1);
                firstNode.Tag = firstItem.Id;

                prmDropDownList.Nodes.Insert(0, firstNode);
                prmDropDownList.SelectedNode = firstNode;
            }

            prmDropDownList.ExpandAll();
        }


        private static List<ComboTreeNode> ProcessDataForComboTreeBox(List<KeyValueDTOForTree> pDataToIterate, string pParentId)
        {
            List<ComboTreeNode> nodes = new List<ComboTreeNode>();

            var query = from i in pDataToIterate
                        where i.ParentId == pParentId
                        orderby i.Value1 ascending
                        select i;

            foreach (var item in query)
            {
                ComboTreeNode node = new ComboTreeNode();
                node.Tag = item.Id;
                node.Text = item.Value1;
                node.Nodes.AddRange(ProcessDataForComboTreeBox(pDataToIterate, item.Id));
                // item.Level = pLevel;
                // pResults.Add(item);
                // ProcessData(pDataToIterate, item.Id, pResults, pLevel + 1);
                nodes.Add(node);
            }

            return nodes;
        }

        public static void LoadUltraComboEditorList(UltraComboEditor prmDropDownList, string prmDataTextField = null, string prmDataValueField = null, List<KeyValueDTO> prmDataSource = null, DropDownListAction? prmDropDownListAction = null)
        {

            prmDropDownList.DataSource = null;
            prmDropDownList.Items.Clear();
            KeyValueDTO firstItem = null;
            var priorFirstItem = prmDataSource.FirstOrDefault(p => p.Id == "-1");

            if (prmDropDownListAction != null)
            {
                switch (prmDropDownListAction)
                {
                    case DropDownListAction.All:
                        firstItem = new KeyValueDTO() { Id = Constants.AllValue, Value1 = Constants.All };
                        break;
                    case DropDownListAction.Select:
                        firstItem = new KeyValueDTO() { Id = Constants.SelectValue, Value1 = Constants.Select };
                        break;
                }
            }

            if (priorFirstItem == null && firstItem != null)
            {
                prmDataSource.Insert(0, firstItem);
            }

            if (prmDataSource != null)
            {
                if (prmDataSource.Count != 0)
                {
                    prmDropDownList.DisplayMember = prmDataTextField;
                    prmDropDownList.ValueMember = prmDataValueField;
                    prmDropDownList.DataSource = prmDataSource;
                }
            }

        }

        public static void LoadUltraComboList(UltraCombo prmDropDownList, string prmDataTextField = null, string prmDataValueField = null, List<GridKeyValueDTO> prmDataSource = null, DropDownListAction? prmDropDownListAction = null)
        {
            GridKeyValueDTO firstItem = null;
            var priorFirstItem = prmDataSource.FirstOrDefault(p => p.Id == "-1");

            if (prmDropDownListAction != null)
            {
                switch (prmDropDownListAction)
                {
                    case DropDownListAction.All:
                        if (prmDataTextField == "Value1")
                        {
                            firstItem = new GridKeyValueDTO() { Id = Constants.AllValue, Value1 = Constants.All };
                        }
                        //else if (prmDataTextField == "Value2")
                        //{
                        //    firstItem = new GridKeyValueDTO() { Id = Constants.AllValue, Value2 = Constants.All };
                        //}
                        break;
                    case DropDownListAction.Select:
                        if (prmDataTextField == "Value1")
                        {
                            firstItem = new GridKeyValueDTO() { Id = Constants.SelectValue, Value1 = Constants.Select };
                        }
                        //else if (prmDataTextField == "Value2")
                        //{
                        //    firstItem = new GridKeyValueDTO() { Id = Constants.SelectValue, Value2 = Constants.Select };
                        //}

                        break;
                }
            }
            if (priorFirstItem == null && firstItem != null)
            {
                prmDataSource.Insert(0, firstItem);
            }

            if (prmDataSource != null)
            {
                if (prmDataSource.Count != 0)
                {
                    prmDropDownList.DataSource = prmDataSource;
                    prmDropDownList.DisplayMember = prmDataTextField;
                    prmDropDownList.ValueMember = prmDataValueField;
                }
            }
        }
       

        public class CustomizedToolTip
        {
            #region Properties

            public Infragistics.Win.UltraWinGrid.UltraGrid UltraDataGrid { set; get; }
            public int AutoPopDelay { set; get; }
            public int AutomaticDelay { set; get; } 
            public string ToolTipMessage { set; get; }
           
            #endregion
           
            #region Tooltip

            // the tooltip that we will use when the cursor is over a cell of the grid
            private System.Windows.Forms.ToolTip tooltip = null;

            // this allows our tooltips to have a delay before appearing
            private Timer timer = new Timer();

            // the message that will be put in the tooltip
            //private string msg = String.Empty;

            public CustomizedToolTip(Infragistics.Win.UltraWinGrid.UltraGrid dataGrid)
            {
                UltraDataGrid = dataGrid;
                // prevent the grid from showing its own tooltips
                UltraDataGrid.DisplayLayout.Override.TipStyleCell = TipStyle.Hide;

                // set this value to however many milliseconds the tooltip delay should be
                timer.Interval = 500;

                // when the timer ticks we want our method to be called
                timer.Tick += new EventHandler(OnTimerTick);
            }
        
            private void OnTimerTick(object sender, EventArgs e)
            {
                tooltip = new System.Windows.Forms.ToolTip();
                tooltip.AutomaticDelay = AutomaticDelay;
                tooltip.AutoPopDelay = AutoPopDelay;           
                tooltip.SetToolTip(UltraDataGrid, ToolTipMessage);
                // once the timer has ticked, stop it
                timer.Stop();
            }

            public void StartTimerToolTip()
            {
                timer.Start();
            }

            public void StopTimerToolTip()
            {
                timer.Stop();
            }

            public void DestroyToolTip(Control ctrl)
            {
                if (tooltip != null)
                {
                    tooltip.SetToolTip(ctrl, String.Empty);
                    tooltip.Dispose();
                    tooltip = null;
                }
            }
         

            #endregion

        }

       
    }

    public static class Globals
    {
        public static ClientSession ClientSession { get; set; }
        
        
    }
}
