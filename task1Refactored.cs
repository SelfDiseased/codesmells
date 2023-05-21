void checkSecurityRefactored(string[] people) {
  foreach (string person in people) {
    if (isSuspiciousPerson(person)) {
      sendAlert();
      return;
    }
  }
}

bool isSuspiciousPerson(string person) {
  string[] suspiciousPeople = { "Don", "John" };
  return suspiciousPeople.Contains(person);
}