using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using FineUI;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Drawing;
using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BE;
using System.Data;
using System.ComponentModel;
using System.Drawing.Imaging;

namespace Sigesoft.Server.WebClientAdmin.UI
{
    public class Utils
    {
        public static void LoadDropDownList(FineUI.DropDownList prmDropDownList, string prmDataTextField = null, string prmDataValueField = null, object prmDataSource = null, DropDownListAction? prmDropDownListAction = null)
        {
            prmDropDownList.Items.Clear();
            FineUI.ListItem firstItem = null;
            if (prmDropDownListAction != null)
            {
                //prmDropDownList. AppendDataBoundItems = true;
               
                switch (prmDropDownListAction)
                {
                    case DropDownListAction.All:
                        firstItem = new FineUI.ListItem(Constants.All, Constants.AllValue);
                        break;
                    case DropDownListAction.Select:
                        firstItem = new FineUI.ListItem(Constants.Select, Constants.SelectValue);
                        break;
                }
            }

            if (prmDataSource != null)
            {
                prmDropDownList.DataTextField = prmDataTextField;
                prmDropDownList.DataValueField = prmDataValueField;
                prmDropDownList.DataSource = prmDataSource;
            }

            prmDropDownList.DataBind();

            if (firstItem != null)
            {
                firstItem.Selected = true;
                prmDropDownList.Items.Insert(0, firstItem);
            }
            
        }

        public static string ExtractFormName(string pstrPath)
        {          
            return pstrPath.Remove(pstrPath.LastIndexOf(".")).Substring(pstrPath.LastIndexOf("/") + 1);
        }

        #region FineUI.Tree

        public static void NodeParentsCheck(FineUI.TreeNode pNode)
        {
            FineUI.TreeNode _node = null;
            _node = pNode.ParentNode;
            _node.Checked = pNode.Checked;
            if (_node.ParentNode != null)
            {
                NodeParentsCheck(_node);
            }
        }

        public static void NodeParentsUnCheck(FineUI.TreeNode Node)
        {
            bool _Found = false;
            foreach (FineUI.TreeNode sNode in Node.Nodes)
            {
                if (sNode.Checked)
                {
                    _Found = true;
                    break;
                }
            }
            if (_Found == false)
            {
                Node.Checked = false;
                if (Node.ParentNode != null)
                {
                    NodeParentsUnCheck(Node.ParentNode);
                }
            }
        }

        public static FineUI.Tree loadTreeMenu(FineUI.Tree tvMenu, List<applicationhierarchyDto> applicationHierarchys)
        {
            if (applicationHierarchys.Count > 0)
            {
                var parents = applicationHierarchys.FindAll(p => p.i_ParentId == -1);

                foreach (var parent in parents)
                {
                    FineUI.TreeNode parentNode = new FineUI.TreeNode();
                    parentNode.Text = parent.v_Description;
                    parentNode.AutoPostBack = true;
                    parentNode.EnableCheckBox = true;
                    parentNode.NodeID = parent.i_ApplicationHierarchyId.ToString();
                    tvMenu.Nodes.Add(parentNode);

                    loadTreeSubMenu(ref parentNode, parent.i_ApplicationHierarchyId, applicationHierarchys);
                }
            }

            tvMenu.ExpandAllNodes();

            return tvMenu;
        }

        public static void loadTreeSubMenu(ref FineUI.TreeNode parentNode, int parentId, List<applicationhierarchyDto> applicationHierarchys)
        {
            var childs = applicationHierarchys.FindAll(p => p.i_ParentId == parentId);

            foreach (var item in childs)
            {
                FineUI.TreeNode child = new FineUI.TreeNode();
                child.Text = item.v_Description;
                child.AutoPostBack = true;
                child.EnableCheckBox = true;
                child.NodeID = item.i_ApplicationHierarchyId.ToString();
                child.ToolTip = item.v_Description;

                parentNode.Nodes.Add(child);
                // Llamada recursiva
                loadTreeSubMenu(ref child, item.i_ApplicationHierarchyId, applicationHierarchys);
            }
        }

        public static FineUI.Tree loadTreeMenuAuthorized(FineUI.Tree tvMenu, List<AutorizationList> applicationHierarchys)
        {
            if (applicationHierarchys.Count > 0)
            {
                var parents = applicationHierarchys.FindAll(p => p.I_ParentId == -1);

                foreach (var parent in parents)
                {
                    FineUI.TreeNode parentNode = new FineUI.TreeNode();
                    parentNode.Text = parent.V_Description;                               
                    parentNode.NodeID = parent.I_ApplicationHierarchyId.ToString();
                    //parentNode.NavigateUrl = string.IsNullOrEmpty(parent.V_Form) ? "" : parent.V_Form;
                    parentNode.ToolTip = parent.V_Description;

                    tvMenu.Nodes.Add(parentNode);

                    loadTreeSubMenuAuthorized(ref parentNode, parent.I_ApplicationHierarchyId, applicationHierarchys);
                }
            }

            tvMenu.ExpandAllNodes();

            return tvMenu;
        }

        public static void loadTreeSubMenuAuthorized(ref FineUI.TreeNode parentNode, int parentId, List<AutorizationList> applicationHierarchys)
        {
            var childs = applicationHierarchys.FindAll(p => p.I_ParentId == parentId);

            foreach (var item in childs)
            {
                FineUI.TreeNode child = new FineUI.TreeNode();
                child.Text = item.V_Description;            
                child.NodeID = item.I_ApplicationHierarchyId.ToString();
                child.ToolTip = item.V_Description;
                if (child.NodeID == "2")
                {

                }
                else
                {
                    child.NavigateUrl = string.IsNullOrEmpty(item.V_Form) ? "" : item.V_Form;        
                }
                 
                child.Target = "main";

                parentNode.Nodes.Add(child);

                // Llamada recursiva
                loadTreeSubMenuAuthorized(ref child, item.I_ApplicationHierarchyId, applicationHierarchys);
            }
        }


     
        #endregion


        public static DataTable ConvertToDatatable<T>(List<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else
                    table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static byte[] imageToByteArray1(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Bmp);
            return ms.ToArray();
        }
    }
}
