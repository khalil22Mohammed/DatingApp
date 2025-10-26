export type User = {
  Id: string;
  DisplayName: string;
  Email: string;
  token: string;
  ImageUrl?: string;
};

export type LoginCreds = {
    email: string;
    password: string;
};
export type RegisterCreds = {
    email: string;
    displayName: string;
    password: string;
};