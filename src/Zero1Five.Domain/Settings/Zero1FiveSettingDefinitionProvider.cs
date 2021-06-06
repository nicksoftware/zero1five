using Volo.Abp.Settings;

namespace Zero1Five.Settings
{
    public class Zero1FiveSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(Zero1FiveSettings.MySetting1));
        }
    }
}
