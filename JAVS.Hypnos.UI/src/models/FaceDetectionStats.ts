import { FaceRect } from '@/models/FaceRect';

export interface FaceDetectionStats
{
    IsZeroFaceAlert: boolean;
    FaceRectangles: FaceRect[];
}