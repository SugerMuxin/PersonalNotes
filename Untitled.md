```heatmap-tracker
heatmapTitle: tracker2025
heatmapSubtitle: ""
property: ""
year: 2025
separateMonths: true
showCurrentDayBorder: true
disableFileCreation: false
colorScheme:
  paletteName: obsidianTheme
ui: {}
```


// Update this object
const trackerData = {
    entries: [],
    separateMonths: true,
    heatmapTitle: "This is the title for your heatmap",
    heatmapSubtitle: "This is the subtitle for your heatmap. You can use it as a description.",
}

// Path to the folder with notes
const PATH_TO_YOUR_FOLDER = "daily notes preview/notes";
// Name of the parameter you want to see on this heatmap
const PARAMETER_NAME = 'steps';

// You need dataviewjs plugin to get information from your pages
for(let page of dv.pages(`"${PATH_TO_YOUR_FOLDER}"`).where((p) => p[PARAMETER_NAME])){
    trackerData.entries.push({
        date: page.file.name,
        // Use absolute file path so clicks open the exact note (for cases when you have multiple notes with the same name)
        filePath: page.file.path,
        intensity: page[PARAMETER_NAME],
    });
}

// Optional: set base path so new files are created here if missing
trackerData.basePath = PATH_TO_YOUR_FOLDER;

renderHeatmapTracker(this.container, trackerData);
```heatmap-tracker
property: <tracker2026>
```
