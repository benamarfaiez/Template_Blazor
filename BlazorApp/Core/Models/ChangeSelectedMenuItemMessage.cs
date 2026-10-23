using BlazorApp.Tool.SmartDetection;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace BlazorApp.Core.Models;

public sealed class ChangeSelectedMenuItemMessage : ValueChangedMessage<GuiToolInstance>
{
    public ChangeSelectedMenuItemMessage(GuiToolInstance tool)
        : base(tool)
    {
        Guard.IsNotNull(tool);
        SmartDetectionInfo = null;
    }

    public ChangeSelectedMenuItemMessage(SmartDetectedTool smartDetectionInfo)
        : base(smartDetectionInfo.ToolInstance)
    {
        Guard.IsNotNull(smartDetectionInfo);
        SmartDetectionInfo = smartDetectionInfo;
    }

    public SmartDetectedTool? SmartDetectionInfo { get; }
}
