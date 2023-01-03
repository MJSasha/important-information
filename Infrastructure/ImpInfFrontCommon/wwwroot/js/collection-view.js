(function () {
    window.collectionViewObservers = {};
    window.collectionView = async function (lastItemIndicator, componentInstance) {

        const options = {
            root: findClosestScrollContainer(lastItemIndicator),
            rootMargin: '0px',
            threshold: 0,
        };

        const observer = new IntersectionObserver(async (entries) => {
            // When the lastItemIndicator element is visible => invoke the C# method `LoadMoreItems`
            for (const entry of entries) {
                if (entry.isIntersecting) {
                    await componentInstance.invokeMethodAsync("LoadMoreItems");
                }
            }
        }, options);

        observer.observe(lastItemIndicator);

        window.collectionViewObservers[componentInstance] = observer;
    };
    window.collectionViewDispose = function (componentInstance) {
        if (window.collectionViewObservers[componentInstance])
            window.collectionViewObservers[componentInstance].disconnect();
        window.collectionViewObservers[componentInstance] = null;
    };
    window.collectionViewItemsChanged = function (lastItemIndicator, componentInstance) {
        const instance = window.collectionViewObservers[componentInstance];
        if (instance) {
            instance.unobserve(lastItemIndicator);
            instance.observe(lastItemIndicator);
        }
    }
})();

// Find the parent element with a vertical scrollbar
// This container should be use as the root for the IntersectionObserver
function findClosestScrollContainer(element) {
    while (element) {
        const style = getComputedStyle(element);
        if (style.overflowY !== 'visible') {
            return element;
        }
        element = element.parentElement;
    }
    return null;
}