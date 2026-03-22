CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TABLE "Jobs" (
    "Id" uuid NOT NULL,
    "ExternalId" text NOT NULL,
    "Title" text NOT NULL,
    "Company" text NOT NULL,
    "SalaryRange" text,
    "Url" text NOT NULL,
    "Description" text NOT NULL,
    "MentionedBlueCard" boolean NOT NULL,
    "Status" integer NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Jobs" PRIMARY KEY ("Id")
);

CREATE UNIQUE INDEX "IX_Jobs_ExternalId" ON "Jobs" ("ExternalId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260321152033_InitialCreate', '10.0.5');

COMMIT;

