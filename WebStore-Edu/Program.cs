#region ���������� ����������

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllersWithViews();
// ������: ������ �� services.AddMvc(); ? 

var app = builder.Build();

#endregion


#region �������� ��������

// ���������� �������� ����������
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.MapGet("/", () => "������!");

#endregion

// ������ ����������
app.Run();
