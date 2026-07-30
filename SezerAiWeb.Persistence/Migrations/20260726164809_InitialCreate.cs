using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SezerAiWeb.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BackupLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BackupType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BackupStartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BackupCompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsSuccess = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ErrorMessage = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    BackupLocation = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    BackupSize = table.Column<long>(type: "bigint", nullable: true),
                    BackupFileName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    BackupMethod = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Automatic"),
                    CanRestore = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ChecksumHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    AdditionalDataJson = table.Column<string>(type: "jsonb", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackupLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogYazilari",
                columns: table => new
                {
                    Baslik = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Slug = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Ozet = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IcerikHtml = table.Column<string>(type: "text", nullable: false),
                    KapakGorseli = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Yazar = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    YayinTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MetaBaslik = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    MetaAciklama = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    MetaAnahtarKelimeler = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogYazilari", x => x.Baslik);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsSystemRole = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemHealths",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CheckedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "Healthy"),
                    CpuUsage = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    MemoryUsage = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    DiskUsage = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    IsDatabaseOnline = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    DatabaseResponseTime = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    IsCacheOnline = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CacheResponseTime = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    IsEmailServiceOnline = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    IsStorageServiceOnline = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    IsGoogleServicesOnline = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    ActiveUsers = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    QueuedJobs = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    FailedJobs = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    AdditionalDataJson = table.Column<string>(type: "jsonb", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemHealths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ProfilePicture = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsEmailVerified = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    LastLoginAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RefreshToken = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecurityLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    EventType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IpAddress = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    UserAgent = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Location = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    IsSuccess = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    FailureReason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AdditionalData = table.Column<string>(type: "jsonb", nullable: true),
                    Severity = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "Info"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Websites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Domain = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    LogoUrl = table.Column<string>(type: "text", nullable: true),
                    FaviconUrl = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    WebsiteTipi = table.Column<int>(type: "integer", nullable: false),
                    ConnectionString = table.Column<string>(type: "text", nullable: true),
                    ApiEndpoint = table.Column<string>(type: "text", nullable: true),
                    ApiKey = table.Column<string>(type: "text", nullable: true),
                    SslExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DomainExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SslProvider = table.Column<string>(type: "text", nullable: true),
                    ContactEmail = table.Column<string>(type: "text", nullable: true),
                    ContactPhone = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    SocialMediaLinks = table.Column<string>(type: "text", nullable: true),
                    GoogleAnalyticsId = table.Column<string>(type: "text", nullable: true),
                    GoogleSearchConsoleId = table.Column<string>(type: "text", nullable: true),
                    GoogleTagManagerId = table.Column<string>(type: "text", nullable: true),
                    MetaTitle = table.Column<string>(type: "text", nullable: true),
                    MetaDescription = table.Column<string>(type: "text", nullable: true),
                    MetaKeywords = table.Column<string>(type: "text", nullable: true),
                    Theme = table.Column<string>(type: "text", nullable: true),
                    Language = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true, defaultValue: "tr-TR"),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true, defaultValue: "TRY"),
                    TimeZone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValue: "Europe/Istanbul"),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Websites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Websites_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AIAgentLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AgentName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TaskType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    InputData = table.Column<string>(type: "jsonb", nullable: true),
                    OutputData = table.Column<string>(type: "jsonb", nullable: true),
                    IsSuccess = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ErrorMessage = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: true),
                    TokensUsed = table.Column<int>(type: "integer", nullable: true),
                    Cost = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: true),
                    WebsiteId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIAgentLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AIAgentLogs_Websites_WebsiteId",
                        column: x => x.WebsiteId,
                        principalTable: "Websites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AlertNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "Info"),
                    Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Priority = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "Normal"),
                    ActionUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ActionText = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    WebsiteId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlertNotifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlertNotifications_Websites_WebsiteId",
                        column: x => x.WebsiteId,
                        principalTable: "Websites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoogleServiceLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ActionType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RequestData = table.Column<string>(type: "jsonb", nullable: true),
                    ResponseData = table.Column<string>(type: "jsonb", nullable: true),
                    IsSuccess = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ErrorMessage = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    StatusCode = table.Column<int>(type: "integer", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: true),
                    WebsiteId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoogleServiceLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoogleServiceLogs_Websites_WebsiteId",
                        column: x => x.WebsiteId,
                        principalTable: "Websites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceMetrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WebsiteId = table.Column<Guid>(type: "uuid", nullable: true),
                    MeasuredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    MetricType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EndpointOrPage = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ResponseTime = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    MinResponseTime = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    MaxResponseTime = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    AvgResponseTime = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TotalRequests = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    SuccessfulRequests = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    FailedRequests = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    MemoryUsed = table.Column<long>(type: "bigint", nullable: true),
                    CpuTime = table.Column<int>(type: "integer", nullable: true),
                    AdditionalDataJson = table.Column<string>(type: "jsonb", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceMetrics_Websites_WebsiteId",
                        column: x => x.WebsiteId,
                        principalTable: "Websites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SeoReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WebsiteId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OverallScore = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    TechnicalScore = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    ContentScore = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    PerformanceScore = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    MobileScore = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    HasSitemap = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    HasRobotsTxt = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    HasSSL = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    BrokenLinks = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    TotalPages = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    IndexedPages = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    DuplicateContent = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    MissingMetaTitles = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    MissingMetaDescriptions = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    PageLoadTime = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TotalPageSize = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    RecommendationsJson = table.Column<string>(type: "jsonb", nullable: true),
                    IssuesJson = table.Column<string>(type: "jsonb", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeoReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeoReports_Websites_WebsiteId",
                        column: x => x.WebsiteId,
                        principalTable: "Websites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteMetrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WebsiteId = table.Column<Guid>(type: "uuid", nullable: false),
                    MetricDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PageViews = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    UniqueVisitors = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    BounceRate = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    AverageSessionDuration = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    NewUsers = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    ReturningUsers = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    OrganicSearchTraffic = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    DirectTraffic = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    ReferralTraffic = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    SocialTraffic = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    GoalCompletions = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    ConversionRate = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteMetrics_Websites_WebsiteId",
                        column: x => x.WebsiteId,
                        principalTable: "Websites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebsiteMenus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WebsiteId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Icon = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    OpenInNewTab = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    CssClass = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Target = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    RequiresAuth = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    AllowedRoles = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebsiteMenus_WebsiteMenus_ParentId",
                        column: x => x.ParentId,
                        principalTable: "WebsiteMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WebsiteMenus_Websites_WebsiteId",
                        column: x => x.WebsiteId,
                        principalTable: "Websites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AIAgentLogs_AgentName",
                table: "AIAgentLogs",
                column: "AgentName");

            migrationBuilder.CreateIndex(
                name: "IX_AIAgentLogs_CreatedAt",
                table: "AIAgentLogs",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AIAgentLogs_IsSuccess",
                table: "AIAgentLogs",
                column: "IsSuccess");

            migrationBuilder.CreateIndex(
                name: "IX_AIAgentLogs_TaskType",
                table: "AIAgentLogs",
                column: "TaskType");

            migrationBuilder.CreateIndex(
                name: "IX_AIAgentLogs_WebsiteId",
                table: "AIAgentLogs",
                column: "WebsiteId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertNotifications_Category",
                table: "AlertNotifications",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_AlertNotifications_CreatedAt",
                table: "AlertNotifications",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AlertNotifications_IsRead",
                table: "AlertNotifications",
                column: "IsRead");

            migrationBuilder.CreateIndex(
                name: "IX_AlertNotifications_Priority",
                table: "AlertNotifications",
                column: "Priority");

            migrationBuilder.CreateIndex(
                name: "IX_AlertNotifications_UserId",
                table: "AlertNotifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertNotifications_WebsiteId",
                table: "AlertNotifications",
                column: "WebsiteId");

            migrationBuilder.CreateIndex(
                name: "IX_BackupLogs_BackupStartedAt",
                table: "BackupLogs",
                column: "BackupStartedAt");

            migrationBuilder.CreateIndex(
                name: "IX_BackupLogs_BackupType",
                table: "BackupLogs",
                column: "BackupType");

            migrationBuilder.CreateIndex(
                name: "IX_BackupLogs_CanRestore",
                table: "BackupLogs",
                column: "CanRestore");

            migrationBuilder.CreateIndex(
                name: "IX_BackupLogs_IsSuccess",
                table: "BackupLogs",
                column: "IsSuccess");

            migrationBuilder.CreateIndex(
                name: "IX_BlogYazilari_Slug",
                table: "BlogYazilari",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogYazilari_YayinTarihi",
                table: "BlogYazilari",
                column: "YayinTarihi");

            migrationBuilder.CreateIndex(
                name: "IX_GoogleServiceLogs_ActionType",
                table: "GoogleServiceLogs",
                column: "ActionType");

            migrationBuilder.CreateIndex(
                name: "IX_GoogleServiceLogs_CreatedAt",
                table: "GoogleServiceLogs",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_GoogleServiceLogs_IsSuccess",
                table: "GoogleServiceLogs",
                column: "IsSuccess");

            migrationBuilder.CreateIndex(
                name: "IX_GoogleServiceLogs_ServiceName",
                table: "GoogleServiceLogs",
                column: "ServiceName");

            migrationBuilder.CreateIndex(
                name: "IX_GoogleServiceLogs_WebsiteId",
                table: "GoogleServiceLogs",
                column: "WebsiteId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceMetrics_MeasuredAt",
                table: "PerformanceMetrics",
                column: "MeasuredAt");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceMetrics_MetricType",
                table: "PerformanceMetrics",
                column: "MetricType");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceMetrics_WebsiteId",
                table: "PerformanceMetrics",
                column: "WebsiteId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceMetrics_WebsiteId_MetricType_MeasuredAt",
                table: "PerformanceMetrics",
                columns: new[] { "WebsiteId", "MetricType", "MeasuredAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SecurityLogs_CreatedAt",
                table: "SecurityLogs",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityLogs_EventType",
                table: "SecurityLogs",
                column: "EventType");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityLogs_IsSuccess",
                table: "SecurityLogs",
                column: "IsSuccess");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityLogs_Severity",
                table: "SecurityLogs",
                column: "Severity");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityLogs_UserId",
                table: "SecurityLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SeoReports_OverallScore",
                table: "SeoReports",
                column: "OverallScore");

            migrationBuilder.CreateIndex(
                name: "IX_SeoReports_ReportDate",
                table: "SeoReports",
                column: "ReportDate");

            migrationBuilder.CreateIndex(
                name: "IX_SeoReports_WebsiteId_ReportDate",
                table: "SeoReports",
                columns: new[] { "WebsiteId", "ReportDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SiteMetrics_MetricDate",
                table: "SiteMetrics",
                column: "MetricDate");

            migrationBuilder.CreateIndex(
                name: "IX_SiteMetrics_WebsiteId_MetricDate",
                table: "SiteMetrics",
                columns: new[] { "WebsiteId", "MetricDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemHealths_CheckedAt",
                table: "SystemHealths",
                column: "CheckedAt");

            migrationBuilder.CreateIndex(
                name: "IX_SystemHealths_Status",
                table: "SystemHealths",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId_RoleId",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsActive",
                table: "Users",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteMenus_ParentId",
                table: "WebsiteMenus",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteMenus_WebsiteId_Order",
                table: "WebsiteMenus",
                columns: new[] { "WebsiteId", "Order" });

            migrationBuilder.CreateIndex(
                name: "IX_Websites_Domain",
                table: "Websites",
                column: "Domain",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Websites_IsActive",
                table: "Websites",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Websites_OwnerId",
                table: "Websites",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AIAgentLogs");

            migrationBuilder.DropTable(
                name: "AlertNotifications");

            migrationBuilder.DropTable(
                name: "BackupLogs");

            migrationBuilder.DropTable(
                name: "BlogYazilari");

            migrationBuilder.DropTable(
                name: "GoogleServiceLogs");

            migrationBuilder.DropTable(
                name: "PerformanceMetrics");

            migrationBuilder.DropTable(
                name: "SecurityLogs");

            migrationBuilder.DropTable(
                name: "SeoReports");

            migrationBuilder.DropTable(
                name: "SiteMetrics");

            migrationBuilder.DropTable(
                name: "SystemHealths");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "WebsiteMenus");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Websites");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
