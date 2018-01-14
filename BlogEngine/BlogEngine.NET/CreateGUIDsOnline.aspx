<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateGUIDsOnline.aspx.cs" Inherits="BlogEngine.NET.CreateGUIDsOnline" %>

<asp:content id="Content1" contentplaceholderid="cphBody" runat="Server">
<div class="page-global">
     <div>
			<h1>
				Create GUIDs online</h1>
			<p>
				This is a web-based version of <a target="_blank" href="http://msdn.microsoft.com/en-us/library/kw069h38.aspx" title="Microsoft download page for the GuidGen tool">Microsoft's GuidGen tool</a>
				to generate <a href="http://en.wikipedia.org/wiki/Globally_unique_identifier" target="_blank" title="Wikipedia article about 'Globally Unique Identifiers'">
					GUIDs</a>.</p>
			<div class="myform" style="padding-top: 30px">
				 <div class="form-group">
                    <input type="text" class="form-control" style="text-align:center;font-size:large;" id="YourGuidLabel" placeholder="" readonly="readonly" onclick="this.focus(); this.select();" name="YourGuidLabel" value="<%=guidSring%>">
                 </div>
				<p>
					<a class="btn btn-primary" style="float: right" href="CreateGUIDsOnline.aspx">Generate new GUID</a></p>
			</div>
			<div style="clear: right">
			</div>
		</div>
</div>
</asp:content>
