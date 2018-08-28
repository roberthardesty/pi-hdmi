export interface ClientGroupStats
{
    GroupName: string;
    ConnectedClientGroupCounts: { [key: string]: number };
}