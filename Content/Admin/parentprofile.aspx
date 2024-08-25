﻿<%@ Page Title="" Language="VB" MasterPageFile="~/digidashboard.master" AutoEventWireup="false" CodeFile="parentprofile.aspx.vb" Inherits="Admin_staffprofile" %>


<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <h2>MANAGE PARENTS</h2>

 <asp:Panel ID="pnlAll" runat="server" DefaultButton="button2">
                <div class="row">
                        <div class="col-lg-12">
                            <div class="sparkline12-list shadow-reset mg-t-30">
                                <div class="sparkline12-hd">
                                    <div class="main-sparkline12-hd">
                                       <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                        
                                            
                                        <asp:Label ID="Label1" Font-Bold="true" Font-Size="Larger" runat="server" Text="Parents List"></asp:Label>
											
                                       
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
        <div class="breadcome-heading">
                                       <span> <asp:Textbox class="form-control" ID="txtSearch"   placeholder="Search..." runat="server" style="width:100%;" ></asp:TextBox>
												<a href="#" id="sb" ><asp:LinkButton TabIndex="0" style="float:right; right:20; top:1; " class="fa fa-search" ID="Button2" runat="server"></asp:LinkButton> </a></span>
                                        </div>
                                    </div>
                                </div>
                                       
                                    </div>
                                </div>
                                <div class="sparkline12-graph">
                                    <div class="basic-login-form-ad">
                                        <div class="row">
                                            <div class="col-lg-12">
            
              <br />
                        
     <div class="form-group-inner">
                                                            <div class="row">
                                                                
                                                                     
                                                                
                                                                                   


                                                               
                                                            </div>
                                                       
   
            
    

          <div class="row">
                   <div class="col-lg-5">
                                                        <div class="comment-replay-profile">
                                                            <div class="btn-group">         
    <asp:Button ID="LinkButton5" class="btn btn-white btn-xs" TabIndex="8"  runat="server" Text="New Parent" ></asp:Button></div></div> </div> </div> 
         <br />
            <div class="comment-replay-profile">
                                                            <div class="btn-group" id="grid" style="width:100%;"> 
                                                                <div class="row">
        <div class="col-lg-6"> 
 <asp:GridView runat="server" AutoGenerateColumns="False" showheader="False" ID="gridview1"  GridLines="Horizontal" class="newtable tableflat" AllowPaging="True">
            <Columns>
                <asp:ImageField DataImageUrlField="passport">
                    <controlstyle BorderStyle="None" Height="80px" Width="80px" />
                </asp:ImageField>
                <asp:BoundField DataField="staffname">
                    <ItemStyle Font-Size="16pt" />
                </asp:BoundField>
                
                
                <asp:CommandField SelectImageUrl="~/image/my-profile.png" SelectText="View" ShowSelectButton="True" ButtonType="Button">
                    <controlstyle cssclass="btn btn-white btn-xs btn-group comment-replay-profile" />
                </asp:CommandField>
            </Columns>
           
    </asp:GridView>
            </div> </div> </div> </div> 
    
    <div class="login-button-pro" style="text-align:right;" >   <asp:Button ID="btnPrevious" runat="server" class="btn btn-sm btn-primary login-submit-cs buttonsnew" Text="Previous" />
      <asp:Button ID="btnNext" runat="server" class="btn btn-sm btn-primary login-submit-cs buttonsnew" Text="Next" /></div>
   








</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        
                    </div>
                     </div> 

                    </div> 
    </asp:Panel>
            
  

      
            <asp:Panel runat="server" id ="panel3" visible="false" style="margin-top: 0px" >
              
    
                                     <div class="row">
                        <div class="col-lg-12">
                            <div class="sparkline12-list shadow-reset mg-t-30">
                                <div class="sparkline12-hd">
                                    <div class="main-sparkline12-hd">
                                        <h1>Parents Profile</h1>
                                       
                                    </div>
                                </div>
                                <div class="sparkline12-graph">
                                    <div class="basic-login-form-ad">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="row">
                   <div class="col-lg-5">
                                                        <div class="comment-replay-profile">
                                                            <div class="btn-group">         
    <asp:Button ID="Button3" class="btn btn-white btn-xs"  runat="server" Text="All Parents"></asp:Button></div></div> </div> </div> 


    <div class="row"><div class="row" id="dd" style="margin:5px;text-align:center;">
                        <span class="picspan" "  >  <asp:Image  ID="Image1" ImageUrl="~/img/noimage.jpg" CssClass="picsupload"  runat="server" /> </span> <br /> </div> 
                           </div>
                                
                                <div class="row">
                                                    <div class="col-lg-5">
                                                        <div class="comment-replay-profile" >
                                                            <div class="btn-group">
                                                                <asp:Button ID="LinkButton4" runat="server" class="btn btn-white btn-xs" Text="Change Passport"></asp:Button>
                                                                <asp:Button ID="lnkPwd" runat="server" class="btn btn-white btn-xs" Text="Reset Password"></asp:Button>
                                                                <asp:Button id="lnkmsg" runat="server" class="btn btn-white btn-xs" Text="Send Message"></asp:Button>
                                                               
                                                            </div>
                                                        </div>
                                                    </div>
                                                   
                                                </div>
                                 <br />
                           
                            <asp:DetailsView ID="DetailsView1" runat="server" class="table"  GridLines="None">
                                <EditRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                            </asp:DetailsView>
                            


                             <asp:CheckBox ID="chkActivated" runat="server" autopostback="true" />
                            



                          



                            
                            



                          
                            


<div class="row">
                   <div class="col-lg-5">
                                                        <div class="comment-replay-profile">
                                                            <div class="btn-group">         
    <asp:Button ID="LinkButton1" class="btn btn-white btn-xs"  runat="server" Text="Edit Profile"></asp:Button></div></div> </div> </div> 
      
    

</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        
                    </div>
                     </div> 
                 

       <div class="row">
                        <div class="col-lg-12">
                            <div class="sparkline12-list shadow-reset mg-t-30">
                                <div class="sparkline12-hd">
                                    <div class="main-sparkline12-hd">
                                        <h1>Children/Wards</h1>
                                       
                                    </div>
                                </div>
                                <div class="sparkline12-graph">
                                    <div class="basic-login-form-ad">
                                        <div class="row">
                                            <div class="col-lg-12">
 <div class="all-form-element-inner">
      <div id ="taught" runat ="server" ></div>



</div>
                                        </div>
                                    </div>
                                </div>
      </div>                      </div>
                        
                    </div>
                     </div> 









    
                 </asp:Panel>
       
              
</asp:Content>

