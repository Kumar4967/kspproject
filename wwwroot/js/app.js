window.setupClickOutsideHandler = (dotNetHelper) => {
    setTimeout(() => {
        const handleClickOutside = (event) => {
            const dropdown = document.querySelector('.dropdown');
            if (dropdown && !dropdown.contains(event.target)) {
                dotNetHelper.invokeMethodAsync('CloseDropdown');
            }
        };
        document.addEventListener('click', handleClickOutside);
        window.dropdownClickHandler = handleClickOutside;
    }, 100);
};
