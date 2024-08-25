﻿<%@ Page Title="" Language="VB" MasterPageFile="~/DigicontentApp.master" ValidateRequest=false AutoEventWireup="false" CodeFile="dnewassignment.aspx.vb" Inherits="Admin_adminpage" %>
   <%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ Register Assembly="SlimeeLibrary" Namespace="SlimeeLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
    <style type="text/css">
        #img3 {
            text-align: center;
        }
        #img4 {
            text-align: center;
        }
        #img2 {
            text-align: center;
        }
        #img1 {
            text-align: center;
        }
        .auto-style2 {
           
            text-align: center;
            width:25%;
            height:25%;
            max-height:25%;
            max-width:25%;
        }
        .auto-style3 {
            width: 29%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:TextBox runat="server" id="att1" visible="false"></asp:TextBox><asp:TextBox id="att2" visible="false" runat="server"></asp:TextBox><asp:TextBox id="att3" visible="false" runat="server"></asp:TextBox><asp:TextBox id="att4" visible="false" runat="server"></asp:TextBox>
    
     <h2>ASSIGNMENTS</h2>
         <div class="row">
                        <div class="col-lg-12">
                            <div class="sparkline12-list shadow-reset mg-t-30">
                                <div class="sparkline12-hd">
                                    <div class="main-sparkline12-hd">
                                        <h1>CHECK ASSIGNMENT</h1>
                                       
                                    </div>
                                </div>
                                <div class="sparkline12-graph">
                                    <div class="basic-login-form-ad">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                    <div class="all-form-element-inner">

  <div class="row">
                   <div class="col-lg-5">
                                                        <div class="comment-replay-profile">
                                                            <div class="btn-group">         
    <asp:Button ID="Button1" class="btn btn-white btn-xs" TabIndex="8"  runat="server" Text="Back" ></asp:Button>
                                                                                                                                

                                                            </div></div> </div> </div> 
<br />
                                                        <div class="form-group-inner">
                                                            <div class="row">
                                                                <div class="col-lg-3">
                                                                    <label class="login2 pull-right pull-right-pro">Title</label>
                                                                </div>
                                                                <div class="col-lg-9">
                                                                     <asp:Textbox cssclass="form-control" ID="txtSubject" runat="server"  ></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                         <div class="form-group-inner">
<div class="row">
                                                                <div class="col-lg-3">
                                                                    <label class="login2 pull-right pull-right-pro">Subject</label>
                                                                </div>
                                                                <div class="col-lg-9">
                                                                    <div class="form-select-list">
                                                                       <asp:Textbox cssclass="form-control" ID="cboSubject" runat="server"  ></asp:TextBox>
                                                                     </div>
                                                                </div>
                                                            </div></div>
                                                         <div class="form-group-inner">
<div class="row">
                                                                <div class="col-lg-3">
                                                                    <label class="login2 pull-right pull-right-pro">Class</label>
                                                                </div>
                                                                <div class="col-lg-9">
                                                                    <div class="form-select-list">
                                                                        <asp:Textbox cssclass="form-control" ID="cboClass" runat="server"  ></asp:TextBox>
                                                                     </div>
                                                                </div>
                                                            </div></div>
                                                       <div class="form-group-inner">
                                                            <div class="row">
                                                                <div class="col-lg-3">
                                                                    <label class="login2 pull-right pull-right-pro">Deadline</label>
                                                                </div>
                                                                <div class="col-lg-9">
                                                                     <asp:TextBox ID="txtDeadline" class="form-control" runat="server"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
            TargetControlID="txtDeadline" Format="dd/MM/yyyy" 
            PopupButtonID="TextBox2" >
        </cc1:CalendarExtender> </div>
                                                            </div>
                                                        </div>
                    
                    







                            <div class="form-group-inner">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                     <FTB:FreeTextBox id="FreeTextBox1" runat="Server" width="100%" EditorBorderColorDark="blue" EditorBorderColorLight="LightBlue" GutterBorderColorDark="Gray" GutterBorderColorLight="LightBlue" ToolbarStyleConfiguration="Office2003" ButtonSet="OfficeMac" DesignModeCss="" ButtonDownImage="True" ButtonOverImage="True" ButtonWidth="35" EnableHtmlMode="False" Focus="True" ReadOnly="True" />

                                                                </div>
                                                            </div>
                                                        </div>


                                                        <br />
                                                       
                                                         <asp:Panel ID="Panel1" runat="server" Visible="False">
        <h4>Attachments</h4>
        <table class="table" id="attachtable" style="border:none; width:100%;" >
            <tr>
                <td id="img1"  runat ="server" class="auto-style2" >
                    <img alt="" src="" id="icon1" style="max-height:100px; width:50%; border:none;" runat ="server"  />
        </td>   
<td id="img2" runat ="server" class="auto-style2" >
        <img alt="" src="" id="icon2" style= "max-height:100px; width:50%; border:none;" runat ="server"  />
        </td>          <td id="img3" runat ="server" class="auto-style2" >
                    <img alt="" src="" id="icon3" style="max-height:100px; width:50%; border:none;" runat ="server"  />
        </td>   
       <td id="img4" runat ="server" class="auto-style2" >
           <img alt="" src="" id="icon4" style="max-height:100px; width:50%; border:none;" runat ="server"  />
        </td>   
                </tr>
            <tr>
                <td class="auto-style2">
                    <asp:LinkButton ID="LinkButton1" runat="server" ></asp:LinkButton></td>
                <td class="auto-style2">
                    <asp:LinkButton ID="LinkButton2" runat="server"></asp:LinkButton></td>
                <td class="auto-style2">
                    <asp:LinkButton ID="LinkButton3" runat="server"></asp:LinkButton></td>
                <td class="auto-style2">
                    <asp:LinkButton ID="LinkButton4" runat="server"></asp:LinkButton></td>
            </tr>
           
            </table>
    </asp:Panel>
 <br />
            
             







</div>
                                        </div>
                        
    
    
    </div> </div> </div> </div> </div> </div> 
    
                   
   
      </asp:Content>