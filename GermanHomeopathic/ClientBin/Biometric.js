function Verify(hdnBiometricContentData) {
    var err;

    try // Exception handling
    {
        DEVICE_AUTO_DETECT = 255;

        var objNBioBSP = new ActiveXObject('NBioBSPCOM.NBioBSP.1');
        var objDevice = objNBioBSP.Device;
        var objExtraction = objNBioBSP.Extraction;

        // You must open device before capture.
        objDevice.Open(DEVICE_AUTO_DETECT);

        err = objDevice.ErrorCode; // Get error code	
        if (err != 0)		// Device open failed
        {
            alert('Device open failed !');
        }
        else {
            // Capture user's fingerprint.
            objExtraction.Capture();
            err = objExtraction.ErrorCode; // Get error code

            if (err != 0)		// Capture failed
            {
                alert('Finger impression capture failed or cancelled !');
            }
            else	// Capture success
            {
                // Get text encoded FIR data from NBioBSP module. 
                hdnBiometricContentData.value = objExtraction.TextEncodeFIR;

                return true;
            }

            // Close device. [AUTO_DETECT]
            objDevice.Close(DEVICE_AUTO_DETECT);
        }

        objExtraction = 0;
        objDevice = 0;
        objNBioBSP = 0;
    }
    catch (e) {
        alert(e.message);
        return false;
    }
    alert(hdnBiometricContentData.value);
    return false;
}


function captureThumbPrint(hdnCtrlId, hdnImgQuality) {
    var err, payload;
    debugger;
    try // Exception handling
    {
        DEVICE_AUTO_DETECT = 255;
        var objNBioBSP = new ActiveXObject('NBioBSPCOM.NBioBSP.1');
        var objDevice = objNBioBSP.Device;
        var objExtraction = objNBioBSP.Extraction;

        // You must open device before enroll.
        objDevice.Open(DEVICE_AUTO_DETECT);

        err = objDevice.ErrorCode; // Get error code	
        if (err != 0)		// Device open failed
        {
            alert('Device open failed !');
        }
        else {
            // Enroll user's fingerprint.
            objExtraction.Enroll(payload);
            err = objExtraction.ErrorCode; // Get error code

            if (err != 0)		// Enroll failed
            {
                alert('Registration failed or cancelled !');
            }
            else	// Enroll success
            {
                // Get text encoded FIR data from NBioBSP module.                        
                var hdnBiometricContentData = document.getElementById(hdnCtrlId);
                hdnBiometricContentData.value = objExtraction.TextEncodeFIR;
                var hdnImageQuality = document.getElementById(hdnImgQuality)
                hdnImageQuality.value = objExtraction.IdentifyImageQuality

                return true;
            }

            // Close device. [AUTO_DETECT]
            objDevice.Close(DEVICE_AUTO_DETECT);
        }

        objExtraction = 0;
        objDevice = 0;
        objNBioBSP = 0;
    }
    catch (e) {
        alert(e.message);
        return false;
    }
    alert(hdnBiometricContentData.value);
    alert(hdnImageQuality.value);
    return false;
}