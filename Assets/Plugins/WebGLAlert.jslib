// This mergeInto line is essential for the WebGL build process.
mergeInto(LibraryManager.library, {
    // Function to show an alert dialog
    ShowAlert: function (str) {
        // Convert the Unity string pointer to a JavaScript string
        var message = Pointer_stringify(str);
        window.alert(message);
    },

    // Function to show a confirm dialog and return the result
    ShowConfirm: function (str) {
        var message = Pointer_stringify(str);
        var result = window.confirm(message);
        // Return a boolean (1 or 0) back to C#
        return result ? 1 : 0;
    }
});