using Volo.Abp.TextTemplating;

namespace Zero1Five.Emailing
{
    public class Zero1FiveTemplateDefinitionProvider:TemplateDefinitionProvider
    {
        public override void Define(ITemplateDefinitionContext context)
        {
            context.Add(
                new TemplateDefinition("Welcome") 
                    .WithVirtualFilePath(
                        "/Emailing/Templates/Welcome.tpl",
                        isInlineLocalized: true
                    )
            );
        }
    }
}