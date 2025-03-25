const homePage = document.getElementById('home-page');
const saveBtn = document.getElementById('save-other-btn');

const getValues = () => ({
    other: {
        homePage: homePage?.value
    }
})

export default function (view, params) {

    // init code here
    view.addEventListener('viewshow', (e) => {
        import("/web/configurationpage?name=shared.js").then((shared) => {
            shared.setPage("Other");
            if (homePage.innerHTML === "") {
                shared.StreamyfinTabs().forEach(tab => homePage.add(new Option(tab.name, tab.resource)))
            }

            homePage.value = shared.getConfig()?.other?.homePage;
            
            shared.setOnConfigUpdatedListener('other', (config) => {
                console.log("updating dom for other")
                const {other} = config;

                homePage.value = other.homePage
            })

            shared.keyedEventListener(saveBtn, 'click', function (e) {
                e.preventDefault();
                const config = shared.getConfig();

                shared.setConfig({
                    ...config,
                    ...getValues()
                });
                shared.saveConfig()
            })
        })
    });
}