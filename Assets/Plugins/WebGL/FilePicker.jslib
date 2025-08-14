mergeInto(LibraryManager.library, {
    OpenXLSXFile: function (callbackFuncNamePtr) {
        const callbackFuncName = UTF8ToString(callbackFuncNamePtr);
        const fileInput = document.getElementById("fileInput");

        fileInput.onchange = function (event) {
            const file = event.target.files[0];
            if (!file) return;

            const reader = new FileReader();
            reader.onload = function (e) {
                const data = new Uint8Array(e.target.result);
                const workbook = XLSX.read(data, { type: "array" });

                // Берём первую страницу и конвертируем в CSV
                const firstSheet = workbook.SheetNames[0];
                const csv = XLSX.utils.sheet_to_csv(workbook.Sheets[firstSheet]);

                // Отправляем в Unity
                SendMessage('FileReceiver', callbackFuncName, csv);
            };
            reader.readAsArrayBuffer(file);
        };

        fileInput.click();
    }
});