using Jellyfin.Data.Enums;

namespace Jellyfin.Plugin.Streamyfin.Configuration.Settings;

public class DownloadOption
{
    public required string label { get; set; }
    public required DownloadQuality value { get; set; }
};

public class LibraryOptions
{
    public DisplayType display { get; set; } = DisplayType.List;
    public CardStyle cardStyle { get; set; } = CardStyle.Detailed;
    public ImageStyle imageStyle { get; set; } = ImageStyle.Cover;
    public bool showTitles { get; set; }
    public bool showStats { get; set; }
};

public class Lockable<T>
{
    public bool? locked { get; set; } = false;
    public T? value { get; set; }
}

/// <summary>
/// Streamyfin application settings
/// </summary>
public class Settings
{
    // Media Controls
    public Lockable<int?>? forwardSkipTime { get; set; } // = 30;
    public Lockable<int?>? rewindSkipTime { get; set; } // = 10;
    
    // Audio
    public Lockable<bool?>? rememberAudioSelections { get; set; } // = true;
    // TODO create type converter for CultureDto
    //  Currently fails since it doesnt have a parameterless constructor
    // public Lockable<CultureDto?>? defaultAudioLanguage { get; set; }
    
    // Subtitles
    // public Lockable<CultureDto?>? defaultSubtitleLanguage { get; set; }
    public Lockable<SubtitlePlaybackMode?>? subtitleMode { get; set; }
    public Lockable<bool?>? rememberSubtitleSelections { get; set; } // = true;
    public Lockable<int?>? subtitleSize { get; set; } // = 80;

    // Other
    public Lockable<bool?>? autoRotate { get; set; } // true
    public Lockable<OrientationLock?>? defaultVideoOrientation { get; set; }
    public Lockable<bool?>? safeAreaInControlsEnabled { get; set; } // = true;
    public Lockable<bool?>? showCustomMenuLinks { get; set; } // = false;
    public Lockable<string[]?>? hiddenLibraries { get; set; } // = [];
    public Lockable<bool?>? disableHapticFeedback { get; set; } // = false;

    // Downloads
    public Lockable<DownloadMethod?>? downloadMethod { get; set; }
    public Lockable<int?>? remuxConcurrentLimit { get; set; }
    public Lockable<bool?>? autoDownload { get; set; } // = false;
    public Lockable<string?>? optimizedVersionsServerUrl { get; set; }
    
    // region Plugins
    // Jellyseerr
    public Lockable<string?>? jellyseerrServerUrl { get; set; }

    // Marlin Search
    public Lockable<SearchEngine?>? searchEngine { get; set; } // = SearchEngine.Jellyfin;
    public Lockable<string?>? marlinServerUrl { get; set; }

    // Popular Lists
    public Lockable<bool?>? usePopularPlugin { get; set; } // = false;
    public Lockable<string[]?>? mediaListCollectionIds { get; set; } // = false;
    // endregion Plugins
    
    // Misc.
    public Lockable<LibraryOptions?>? libraryOptions { get; set; }
    
    // TODO: These are used outside of settings. Review usages/delete any unused later.
    // public Lockable<bool?>? forceLandscapeInVideoPlayer { get; set; }
    // public Lockable<DeviceProfile?>? deviceProfile { get; set; } // = DeviceProfile.Expo;
    // public Lockable<string[]?>? deviceProfile { get; set; } // = [];
    // public Lockable<bool?>? openInVLC { get; set; }
    // public Lockable<DownloadOption?>? downloadQuality { get; set; }
    // public Lockable<bool?>? playDefaultAudioTrack { get; set; } // = true;
    // public Lockable<bool?>? showHomeTitles { get; set; } // = true;
}