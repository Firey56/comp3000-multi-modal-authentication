# Multi-Modal Authentication using Facial Recognition and Facial Analysis
## Project Supervisor: Nathan Clarke


Data security is now in the limelight of media and public attention, especially regarding
personally identifiable information (PII). With a large majority of companies and
businesses possessing some variation of PII. With that, there is a large amount of
cybercrime that focuses on illegally obtaining and selling the information. Mostly, this
information is often leaked through data breaches due to weak credentials or through
stolen credentials through phishing (Kaspersky, How data breaches happen 2023). Current
multi-factor authentication (MFA) methods can be broken due to user interaction. Users
that are under an MFA fatigue attack are considerable risk. 1% of users are found to just
accept an authentication request first try (Defend your users from MFA Fatigue attacks
2023) due to the simplicity in nature. As well as this, alternate forms of MFA such as email
and mobile phone number can be intercepted, especially if a user has repeated use of their
password. Furthermore, other existing MFA methods mostly require you to have an
additional piece of technology to authenticate. This can either be manipulated by an
attacker to be able to steal session tokens if they were able to perform an advisory in the
middle attack.
The form of authentication that I am developing a multi-modal approach and is
designed to be used within public and private sectors that require elevated levels of
authentication to enforce access control and to reduce the risk of impersonation. The
multi-modal approach helps reduce the risk of a singular point of failure (for example a
user’s password being breached). The authentication uses keystroke analysis and learns
off the user’s keystroke patterns to determine a confidence score about whether the
user is legitimately authorising themselves or whether it is unauthorised access. While
it is exceedingly difficult to impersonate keystroke patterns, it is not an impenetrable
method of authentication. The combination of biometrics, in this case facial
recognition, and keystroke analysis is designed to have a high confidence rating on
whether an attempted authentication is a legitimate use-case, or if there is an attempt
to masquerade as another user.
The software will use machine learning to learn and generate confidence scores about
whether a user, who is attempting to authenticate, is replicating keystroke patterns in
how the system will have learned and is therefore expecting. Either allowing the
attempted authentication into the system, or to deny the attempt. In further design of
the system, this would be integrated with a security information event management
(SIEM) to provide alerts of unsuccessful authentication attempts for further
investigation.

## The Code

There are several editable parameters within the application to change for user experience:

### Authentication Model:

- NewSignInForm.cs </br>

  - UserSignIn():</br>
      - finalConfidence - Values can be adjusted for the formula weighting (currently 60/40 split with Facial and Keystroke anaysis)</br>
      - FinalConfidenceCheck - The successful authentication attempt confidence score, currently set at 70% </br>
      - keystrokeAnalysisConfidence - The minimum keystroke analysis success value before it is inserted in to the legitimate training data set.</br>

  - TakePhoto()
     - photoCount - The amount of images that it should take of the user, currently set to 6 (n-1).

- AdminForm.cs </br>
  - currentSession.Confidence - The variable holds the current session confidence, the comparison for admins being able to edit users. Currently set to 80.

 - RealTimeForm.cs </br>
    - UpdateLabelsForLogin()
      - Switch Case: data.Parse - this checks what the confidence rating is for each modality. Facial Recognition and Keystroke Analysis set to 60%, Decision for Authentication set to 70%.

### MachineLearningModel.py

 - New Pattern
     - newRandom - The amount of standard deviations that will be added, currently random between -2 and 2.
     - noise - The amount of random noise that will be added to the keystroke pair, currently between -20 and 20.
