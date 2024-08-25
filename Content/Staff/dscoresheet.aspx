﻿<%@ Page Title="" Language="VB" MasterPageFile="~/digidashboard.master" AutoEventWireup="false" CodeFile="dscoresheet.aspx.vb" Inherits="Staff_scoresheet" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <h2 >CHECK SCORES</h2>
      
     <div class="form-group-inner">
<div class="row">
                                                                <div class="col-lg-3">
                                                                    <label class="login2 pull-right pull-right-pro">Subordinate</label>
                                                                </div>
                                                                <div class="col-lg-9">
                                                                    <div class="form-select-list">
                                                                       <asp:DropDownList cssclass = "form-control custom-select-value" ID="DropDownList1" runat="server" autopostback="true" >
                                                                               
                                                                                </asp:DropDownList>
                                                                     </div>
                                                                </div>
                                                            </div></div>
    
        
  
      <div class="form-group-inner">
<div class="row">
                                                                <div class="col-lg-3">
                                                                    <label class="login2 pull-right pull-right-pro">Subject</label>
                                                                </div>
                                                                <div class="col-lg-9">
                                                                    <div class="form-select-list">
                                                                       <asp:DropDownList cssclass = "form-control custom-select-value" ID="cboSubject" runat="server" autopostback="true"  >
                                                                                
                                                                                </asp:DropDownList>
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
                                                                       <asp:DropDownList cssclass = "form-control custom-select-value" ID="cboClass" runat="server" autopostback="true" >
                                                                                
                                                                                </asp:DropDownList>
                                                                     </div>
                                                                </div>
                                                            </div></div>




    <asp:Panel id="panel1" runat="server" visible="false">
           <div class="row">
                        <div class="col-lg-12">
                            <div class="sparkline12-list shadow-reset mg-t-30">
                                <div class="sparkline12-hd">
                                    <div class="main-sparkline12-hd">
                                        <h1>Class Summary</h1>
                                       
                                    </div>
                                </div>
                                <div class="sparkline12-graph">
                                    <div class="basic-login-form-ad">
                                        <div class="row">
                                            <div class="col-lg-12">
 <div class="all-form-element-inner">


        <table class="newtable" >
            <tr>
                    <td style="width:28%;"><strong>Number of students:</strong></td><td><asp:Label runat="server" id="lblNo"></asp:Label></td>
                </tr>
        <tr>
                    <td style="width:28%;"><strong>Subject Average</strong>:</td><td><asp:Label runat="server" id="lblSA"></asp:Label></td>
                </tr>
            <tr>
                    <td class="auto-style2"><strong>Highest in Class:</strong></td><td><asp:Label runat="server" id="lbhighest"></asp:Label></td>
                </tr>
            <tr>
                    <td class="auto-style2"><strong>Lowest in Class:</strong></td><td><asp:Label runat="server" id="lblLowest"></asp:Label></td>
                </tr>
        </table>
     
</div>
                                        </div>
                                    </div>
                                </div>
      </div>                      </div>
                        
                    </div>
                     </div> 
  
           
                
              <div class="row">
                        <div class="col-lg-12">
                            <div class="sparkline12-list shadow-reset mg-t-30">
                                <div class="sparkline12-hd">
                                    <div class="main-sparkline12-hd">
                                        <h1>Student Scores</h1>
                                       
                                    </div>
                                </div>
                                <div class="sparkline12-graph">
                                    <div class="basic-login-form-ad">
                                        <div class="row">
                                            <div class="col-lg-12">
 <div class="all-form-element-inner">
     <div class="comment-replay-profile">
                                                            <div class="btn-group" id="grid" style="width:100%;">
                <asp:GridView ID="GridView1" GridLines="None" class="newtable" autogeneratecolumns ="false" OnRowUpdating="GridView1_RowUpdating"  runat="server">
                    
      
                    <Columns>
                        <asp:CommandField ShowEditButton="True"><controlstyle cssclass="btn btn-white btn-xs btn-group comment-replay-profile " /></asp:CommandField>
                        <asp:BoundField DataField="Reg No" HeaderText="Reg No" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="1st CA" HeaderText="1st CA"></asp:BoundField>
                        <asp:BoundField DataField="2nd CA" HeaderText="2nd CA"></asp:BoundField>
                        <asp:BoundField DataField="3rd CA" HeaderText="3rd CA"></asp:BoundField>
                        <asp:BoundField DataField="4th CA" HeaderText="4th CA"></asp:BoundField>
                        <asp:BoundField DataField="Exams" HeaderText="Exams"></asp:BoundField>
                        <asp:BoundField DataField="Total" HeaderText="Total" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="Grade" HeaderText="Grade" ReadOnly="True"></asp:BoundField>
                        
                    </Columns>
      
      </asp:GridView></div> </div> 
        
</div>
                                        </div>
                                    </div>
                                </div>
      </div>                      </div>
                        
                    </div>
                     </div>        
      </asp:Panel>
</asp:Content>

