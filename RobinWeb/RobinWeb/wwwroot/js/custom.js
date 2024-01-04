function copyToClipboard(elem) {
    debugger
    // Copy the text inside the text field
    navigator.clipboard.writeText(elem);

    // Alert the copied text
    alert("Copied the text: " + elem);
}