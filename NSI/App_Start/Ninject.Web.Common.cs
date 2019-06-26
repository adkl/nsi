using NSI.RuleEvaluator;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NSI.Api.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NSI.Api.App_Start.NinjectWebCommon), "Stop")]

namespace NSI.Api.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using System.Web.Http;
    using Ninject.Web.WebApi;
    using NSI.Logger.Interfaces;
    using NSI.Logger.Implementations;
    using NSI.Api.Core;
    using System.Web.Http.Filters;
    using Ninject.Web.WebApi.FilterBindingSyntax;
    using System.Reflection;
    using Ninject.Planning.Bindings.Resolvers;
    using NSI.EF;
    using NSI.Repository.Interfaces.Membership;
    using NSI.Repository.Interfaces.Notifications;
    using NSI.Repository.Membership;
    using NSI.Repository.Notifications;
    using NSI.Repository.Interfaces;
    using NSI.BusinessLogic.Interfaces.Membership;
    using NSI.BusinessLogic.Interfaces.Notifications;
    using NSI.BusinessLogic.Membership;
    using NSI.Repository.DeviceManagement;
    using NSI.Repository.Interfaces.DeviceManagement;
    using NSI.BusinessLogic.DeviceManagement;
    using NSI.BusinessLogic.Interfaces.DeviceManagement;
    using NSI.Repository.Interfaces.IncidentManagement;
    using NSI.Repository.IncidentManagement;
    using NSI.BusinessLogic.Interfaces.IncidentManagement;
    using NSI.BusinessLogic.IncidentManagement;
    using NSI.BusinessLogic.Interfaces;
    using NSI.Repository.DevicePing;
    using NSI.Repository.Interfaces.DevicePing;
    using NSI.BusinessLogic.Interfaces.DevicePing;
    using NSI.BusinessLogic.DevicePing;
    using NSI.Repository.Interfaces.RuleEngine;
    using NSI.Repository.RuleEngine;
    using NSI.BusinessLogic.Interfaces.RuleEngine;
    using NSI.BusinessLogic.RuleEngine;
    using NSI.BusinessLogic.Notifications;
    using NSI.Repository.Interfaces.Document;
    using NSI.Repository.DocumentNamespace;
    using NSI.DocumentGenerator.Interfaces.Helpers;
    using NSI.DocumentGenerator.Interfaces.Generators;
    using NSI.DocumentGenerator.Interfaces;
    using NSI.DocumentGenerator.Implementations;
    using NSI.DocumentGenerator.Implementations.Helpers;
    using MassTransit;
    using NSI.Queue.MessageConsumers;
    using Ninject.Activation.Providers;
    using System.Configuration;
    using NSI.Repository.Interfaces.TemplateManagement;
    using NSI.Repository.TemplateManagement;
    using NSI.BusinessLogic.Interfaces.TemplateManagement;
    using NSI.BusinessLogic.TemplateManagement;
    using NSI.DocumentGenerator.Implementations.Generators;
    using NSI.Repository;
    using NSI.Repository.Interfaces.DocumentManagement;
    using NSI.DocumentRepository.Interfaces;
    using NSI.DocumentRepository.Implementations;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<NsiContext>().To<NsiContext>().InRequestScope();
            kernel.Bind<ILoggerAdapter>().To<NLogAdapter>().InSingletonScope();

            // Repositories
            kernel.Bind<IPermissionRepository>().To<PermissionRepository>().InRequestScope();
            kernel.Bind<IModuleRepository>().To<ModuleRepository>().InRequestScope();
            kernel.Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            kernel.Bind<IDeviceTypeRepository>().To<DeviceTypeRepository>().InRequestScope();
            kernel.Bind<IDeviceRepository>().To<DeviceRepository>().InRequestScope();
            kernel.Bind<IIncidentRepository>().To<IncidentRepository>().InRequestScope();
            kernel.Bind<IIncidentSettlementRepository>().To<IncidentSettlementRepository>().InRequestScope();
            kernel.Bind<IIncidentTypeRepository>().To<IncidentTypeRepository>().InRequestScope();
            kernel.Bind<IIncidentPriorityRepository>().To<IncidentPriorityRepository>().InRequestScope();
            kernel.Bind<IIncidentWorkOrderRepository>().To<IncidentWorkOrderRepository>().InRequestScope();
            kernel.Bind<IIncidentStatusRepository>().To<IncidentStatusRepository>().InRequestScope();
            kernel.Bind<IDevicePingRepository>().To<DevicePingRepository>().InRequestScope();
            kernel.Bind<IRoleRepository>().To<RoleRepository>().InRequestScope();
            kernel.Bind<ITenantRepository>().To<TenantRepository>().InRequestScope();
            kernel.Bind<ILanguageRepository>().To<LanguageRepository>().InRequestScope();
            kernel.Bind<IRolePermissionRepository>().To<RolePermissonRepository>().InRequestScope();
            kernel.Bind<IRoleMemberRepository>().To<RoleMemberRepository>().InRequestScope();
            kernel.Bind<IRuleRepository>().To<RuleRepository>().InRequestScope();
            kernel.Bind<IDeviceActionRepository>().To<DeviceActionRepository>().InRequestScope();
            kernel.Bind<IDevicePropertyRepository>().To<DevicePropertyRepository>().InRequestScope();

            kernel.Bind<IDocumentTypeRepository>().To<DocumentTypeRepository>().InRequestScope();
            kernel.Bind<IGeneratedDocumentRepository>().To<GeneratedDocumentRepository>().InRequestScope();
            kernel.Bind<IEmailMessageRepository>().To<EmailMessageRepository>().InRequestScope();
            kernel.Bind<IEmailRecipientRepository>().To<EmailRecipientRepository>().InRequestScope();
            kernel.Bind<IEmailRecipientTypeRepository>().To<EmailRecipientTypeRepository>().InRequestScope();
            kernel.Bind<IAttachmentRepository>().To<AttachmentRepository>().InRequestScope();
            kernel.Bind<INotificationRepository>().To<NotificationRepository>().InRequestScope();
            kernel.Bind<INotificationUserRepository>().To<NotificationUserRepository>().InRequestScope();
            kernel.Bind<INotificationStatusRepository>().To<NotificationStatusRepository>().InRequestScope();
            kernel.Bind<ISmsRepository>().To<SmsRepository>().InRequestScope();
            kernel.Bind<INotificationTypeRepository>().To<NotificationTypeRepository>().InRequestScope();
            kernel.Bind<IWebNotificationRepository>().To<WebNotificationRepository>().InRequestScope();
            kernel.Bind<IDocumentRepository>().To<DocumentRepository>().InRequestScope();
            kernel.Bind<IFileTypeRepository>().To<FileTypeRepository>().InRequestScope();
            kernel.Bind<IStorageTypeRepository>().To<StorageTypeRepository>().InRequestScope();

            kernel.Bind<IFolderRepository>().To<FolderRepository>().InRequestScope();
            kernel.Bind<ITemplateRepository>().To<TemplateRepository>().InRequestScope();
            kernel.Bind<ITemplateVersionRepository>().To<TemplateVersionRepository>().InRequestScope();

            // Business
            kernel.Bind<IPermissionManipulation>().To<PermissionManipulation>().InRequestScope();
            kernel.Bind<IModuleManipulation>().To<ModuleManipulation>().InRequestScope();
            kernel.Bind<IUserManipulation>().To<UserManipulation>().InRequestScope();
            kernel.Bind<IDeviceTypeManipulation>().To<DeviceTypeManipulation>().InRequestScope();
            kernel.Bind<IDeviceManipulation>().To<DeviceManipulation>().InRequestScope();
            kernel.Bind<IIncidentManipulation>().To<IncidentManipulation>().InRequestScope();
            kernel.Bind<IIncidentSettlementManipulation>().To<IncidentSettlementManipulation>().InRequestScope();
            kernel.Bind<IGeneratedDocumentLogger>().To<GeneratedDocumentLogger>().InRequestScope();
            kernel.Bind<IFileGenerator>().To<FileGenerator>().InRequestScope();
            kernel.Bind<IDocumentGenerator>().To<DocumentGenerator>().InRequestScope();
            kernel.Bind<IPdfGenerator>().To<PdfGenerator>().InRequestScope();
            kernel.Bind<IHtmlGenerator>().To<HtmlGenerator>().InRequestScope();
            kernel.Bind<IOdtGenerator>().To<OdtGenerator>().InRequestScope();
            kernel.Bind<IDocxGenerator>().To<DocxGenerator>().InRequestScope();
            kernel.Bind<IHtmlGeneratorHelper>().To<HtmlGeneratorHelper>().InRequestScope();
            kernel.Bind<IIncidentTypeManipulation>().To<IncidentTypeManipulation>().InRequestScope();
            kernel.Bind<IIncidentPriorityManipulation>().To<IncidentPriorityManipulation>().InRequestScope();
            kernel.Bind<IIncidentWorkOrderManipulation>().To<IncidentWorkOrderManipulation>().InRequestScope();
            kernel.Bind<IIncidentStatusManipulation>().To<IncidentStatusManipulation>().InRequestScope();
            kernel.Bind<IDevicePingManipulation>().To<DevicePingManipulation>().InRequestScope();
            kernel.Bind<IPingDeviceManipulation>().To<PingDeviceManipulation>().InRequestScope();
            kernel.Bind<IRoleManipulation>().To<RoleManipulation>().InRequestScope();
            kernel.Bind<IRuleManipulation>().To<RuleManipulation>().InRequestScope();
            kernel.Bind<IDeviceActionManipulation>().To<DeviceActionManipulation>().InRequestScope();
            kernel.Bind<IDevicePropertyManipulation>().To<DevicePropertyManipulation>().InRequestScope();


            kernel.Bind<IEmailMessageManipulation>().To<EmailMessageManipulation>().InRequestScope();
            kernel.Bind<IEmailRecipientManipulation>().To<EmailRecipientManipulation>().InRequestScope();
            kernel.Bind<IEmailRecipientTypeManipulation>().To<EmailRecipientTypeManipulation>().InRequestScope();
            kernel.Bind<IAttachmentManipulation>().To<AttachmentManipulation>().InRequestScope();
            kernel.Bind<INotificationManipulation>().To<NotificationManipulation>().InRequestScope();
            kernel.Bind<INotificationUserManipulation>().To<NotificationUserManipulation>().InRequestScope();
            kernel.Bind<INotificationStatusManipulation>().To<NotificationStatusManipulation>().InRequestScope();
            kernel.Bind<ISmsManipulation>().To<SmsManipulation>().InRequestScope();
            kernel.Bind<INotificationTypeManipulation>().To<NotificationTypeManipulation>().InRequestScope();
            kernel.Bind<IWebNotificationManipulation>().To<WebNotificationManipulation>().InRequestScope();
            kernel.Bind<IDocumentManipulation>().To<DocumentManipulation>().InRequestScope();
            kernel.Bind<IFileTypeManipulation>().To<FileTypeManipulation>().InRequestScope();
            kernel.Bind<IStorageTypeManipulation>().To<StorageTypeManipulation>().InRequestScope();
            kernel.Bind<ITenantManipulation>().To<TenantManipulation>().InRequestScope();
            kernel.Bind<ILanguageManipulation>().To<LanguageManipulation>().InRequestScope();
            kernel.Bind<IRolePermissionManipulation>().To<RolePermissionManipulation>().InRequestScope();
            kernel.Bind<IRoleMemberManipulation>().To<RoleMemberManipulation>().InRequestScope();

            kernel.Bind<IFolderManipulation>().To<FolderManipulation>().InRequestScope();
            kernel.Bind<ITemplateManipulation>().To<TemplateManipulation>().InRequestScope();
            kernel.Bind<ITemplateVersionManipulation>().To<TemplateVersionManipulation>().InRequestScope();
            kernel.Bind<ITemplateGenerator>().To<TemplateGenerator>().InRequestScope();
            kernel.Bind<IExportTemplateManipulation>().To<ExportTemplateManipulation>().InRequestScope();

            // RabbitMq EventBus Bindings
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(ConfigurationManager.AppSettings["rabbitMQHostUri"].ToString()), h =>
                {
                    h.Username(ConfigurationManager.AppSettings["rabbitMQUsername"].ToString());
                    h.Password(ConfigurationManager.AppSettings["rabbitMQPassword"].ToString());
                });
                
                // Define message queues and bind consumers to queue here
                cfg.ReceiveEndpoint(host, "message_log_queue", e =>
                {
                    e.Consumer<LogMessageReceivedConsumer>();
                });

                cfg.ReceiveEndpoint(host, "device_ping_queue", e =>
                {
                    e.Consumer<DevicePingReceivedConsumer>();
                });

                cfg.ReceiveEndpoint(host, "ping_device_queue", e =>
                {
                    e.Consumer<PingDeviceReceivedConsumer>();
                });
            });

            kernel.Bind<IBusControl, IPublishEndpoint>().ToMethod(c =>
            {
                return busControl;
            })
            .InSingletonScope();

            
            kernel.Bind<IBus>().ToProvider(new CallbackProvider<IBus>(x => x.Kernel.Get<IBusControl>()));

            busControl.Start();

            DevicePingListener.MakeInstance(
                (IRuleRepository) kernel.GetService(typeof(IRuleRepository)),
                (IIncidentRepository) kernel.GetService(typeof(IIncidentRepository)),
                (IDevicePingRepository) kernel.GetService(typeof(IDevicePingRepository)),
                (IIncidentTypeRepository) kernel.GetService(typeof(IIncidentTypeRepository))
            );

            // TODO: Do this better :)
            kernel.BindHttpFilter<HandleExceptionFilterAttribute>(FilterScope.Global).WithConstructorArgument("logger", kernel.Get<ILoggerAdapter>());

            kernel.Load(Assembly.GetExecutingAssembly());
            //kernel.Bind(x => x.FromAssembliesMatching("dll name").SelectAllClasses().BindAllInterfaces();
        }
    }
}