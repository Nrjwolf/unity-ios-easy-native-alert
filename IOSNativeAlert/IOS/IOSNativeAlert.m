//
//  IOSNativeAlert.m
//  Alert
//
//  Created by Nrjwolf on 04/12/2019.
//  Copyright © 2019 Nrjwolf. All rights reserved.
//

extern UIViewController *UnityGetGLViewController();

NSString *ToNSString(char* string) {
    return [NSString stringWithUTF8String:string];
}

// Unity button delegate delegate
typedef void (*MonoPAlertButtonDelegate)(const char* buttonId);
static MonoPAlertButtonDelegate _onButtonClick = NULL;

// Register unity message handler
FOUNDATION_EXPORT void IOSRegisterMessageHandler(MonoPAlertButtonDelegate onButtonClick)
{
    _onButtonClick = onButtonClick;
}

// Send message to unity
void SendMessageToUnity(const char* buttonId) {
    NSLog(@"Clicked %s", buttonId);
    if(_onButtonClick != NULL) {
        _onButtonClick(buttonId);
    }
}

// Alert function
void _IOSShowAlertMsg (char* title, char* message, char* buttons[], int buttonsLength) {
    UIAlertController * alert = [UIAlertController alertControllerWithTitle : ToNSString(title) message : ToNSString(message) preferredStyle : UIAlertControllerStyleAlert];

    // Add buttons
    if (buttons && buttonsLength > 0) {
        for (int i = 0; i < buttonsLength; i++) {
            NSString* buttonTitle = ToNSString(buttons[i]);
            UIAlertAction * button = [UIAlertAction actionWithTitle:buttonTitle style:UIAlertActionStyleDefault handler:^(UIAlertAction * action) {
                NSString* buttonId = [NSString stringWithFormat:@"%@%d", buttonTitle, i];
                SendMessageToUnity((char*)[buttonId UTF8String]);
            }];
            [alert addAction:button];
        }
    }
    
    dispatch_async(dispatch_get_main_queue(), ^{
        [UnityGetGLViewController() presentViewController:alert animated:YES completion:nil];
    });
}

/// Show something like android toast
void _IOSShowToast (char* message, BOOL isLongDuration) {
    NSString *messageString = ToNSString(message);
    UIAlertController *alert = [UIAlertController alertControllerWithTitle:messageString message:@"" preferredStyle:UIAlertControllerStyleAlert];
    
    UIWindow *alertWindow = [[UIWindow alloc] initWithFrame:[UIScreen mainScreen].bounds];
    alertWindow.rootViewController = [[UIViewController alloc] init];
    alertWindow.windowLevel = UIWindowLevelAlert + 1;
    [alertWindow makeKeyAndVisible];
    [alertWindow.rootViewController presentViewController:alert animated:YES completion:nil];
    
    float showDuration = isLongDuration ? 1 : .5;
    dispatch_after(dispatch_time(DISPATCH_TIME_NOW, (int64_t)(showDuration * NSEC_PER_SEC)), dispatch_get_main_queue(), ^{
        [alertWindow.rootViewController dismissViewControllerAnimated:YES completion:nil];
    });
}
