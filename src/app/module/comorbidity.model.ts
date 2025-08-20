export interface Comorbidity {
  id?: number; // or whatever IDs you use

  // Add these so filters work:
  zone: string;
  state: number; // or string, depends on your API
  city: number;  // or string, depends on your API

  // Existing fields
  ddPresent: string; // or boolean if your backend uses boolean
  dbPresent: string;
  cldPresent: string;
  ndPresent: string;
  bdPresent: string;

  // Any other fields you already have...
}
